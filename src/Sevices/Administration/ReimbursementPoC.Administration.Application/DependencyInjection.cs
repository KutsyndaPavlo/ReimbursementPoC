using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReimbursementPoC.Administration.Application.Common.Behaviours;
using ReimbursementPoC.Administration.Application.Common.Mappings;
using ReimbursementPoC.Administration.Application.Program.DomainServices;
using ReimbursementPoC.Administration.Domain.Program;
using ReimbursementPoC.Administration.IntergrationEvents;
using System.Reflection;

namespace ReimbursementPoC.Administration.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(MappingProfile.AutoMapperConfig, typeof(MappingProfile).Assembly);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingPipelineBehavior<,>));
            services.AddScoped<IProgramService, ProgramService>();
            services.AddEventBus(configuration);

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var asb_connection_strig = Environment.GetEnvironmentVariable("ASB_Connection_String");
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddServiceBusMessageScheduler();
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                if (!string.IsNullOrWhiteSpace(asb_connection_strig))
                {
                    busConfigurator.UsingAzureServiceBus((context, configurator) =>
                    {
                        configurator.Host(asb_connection_strig);

                        configurator.ConfigureEndpoints(context);

                        configurator.UseServiceBusMessageScheduler();
                        configurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));
                        configurator.Message<ProgramCanceledIntegrationEvent>(m => m.SetEntityName(Constants.ProgramCanceledTopic));
                        configurator.Message<ProgramDeletedIntegrationEvent>(m => m.SetEntityName(Constants.ProgramDeletedTopic));
                        configurator.Message<ProgramCreatedIntegrationEvent>(m => m.SetEntityName(Constants.ProgramCreatedTopic));
                        configurator.Message<ProgramUpdatedIntegrationEvent>(m => m.SetEntityName(Constants.ProgramUpdatedTopic));
                        configurator.Message<ServiceCanceledIntegrationEvent>(m => m.SetEntityName(Constants.ServiceCanceledTopic));
                        configurator.Message<ServiceCreatedIntegrationEvent>(m => m.SetEntityName(Constants.ServiceCreatedTopic));
                        configurator.Message<ServiceDeletedIntegrationEvent>(m => m.SetEntityName(Constants.ServiceDeletedTopic));
                        configurator.Message<ServiceUpdatedIntegrationEvent>(m => m.SetEntityName(Constants.ServiceUpdatedTopic));
                    });
                }
                else
                {
                    busConfigurator.UsingRabbitMq((context, configurator) =>
                    {
                        configurator.Host(Environment.GetEnvironmentVariable("RabbitMqHost") ?? "localhost", "/", h =>
                        {
                            h.Username(Environment.GetEnvironmentVariable("RabbitMqUser"));
                            h.Password(Environment.GetEnvironmentVariable("RabbitMqPass"));
                        });

                        configurator.ConfigureEndpoints(context);
                    });
                }
            });

            return services;
        }
    }
}