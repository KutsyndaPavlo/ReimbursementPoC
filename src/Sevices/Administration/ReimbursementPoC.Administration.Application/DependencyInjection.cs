using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReimbursementPoC.Administration.Application.Common.Behaviours;
using ReimbursementPoC.Administration.Application.Common.Mappings;
using ReimbursementPoC.Administration.Application.Program.DomainServices;
using ReimbursementPoC.Administration.Domain.Program;
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
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                if (!string.IsNullOrWhiteSpace(asb_connection_strig))
                {
                    busConfigurator.UsingAzureServiceBus((context, configurator) =>
                    {
                        configurator.Host(asb_connection_strig);

                        configurator.ConfigureEndpoints(context);
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