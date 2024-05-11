using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReimbursementPoC.Vendor.Application.Common.Behaviours;
using ReimbursementPoC.Vendor.Application.Common.Mappings;
using ReimbursementPoC.Vendor.Application.VendorSubmission.DomainServices;
using ReimbursementPoC.Vendor.Domain.VendorSubmission.DomainServices;
using ReimbursementPoC.Vendor.IntergrationEvents;
using System.Reflection;

namespace ReimbursementPoC.Vendor.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IVendorSubmissionService, VendorSubmissionService>();
            services.AddAutoMapper(MappingProfile.AutoMapperConfig, typeof(MappingProfile).Assembly);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
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

                        configurator.Message<VendorSubmissionCanceledIntegrationEvent>(m => m.SetEntityName(Constants.VendorSubmissionCanceledTopic));
                        configurator.Message<VendorSubmissionCreatedIntegrationEvent>(m => m.SetEntityName(Constants.VendorSubmissionCreatedTopic));
                        configurator.Message<VendorSubmissionDeletedIntegrationEvent>(m => m.SetEntityName(Constants.VendorSubmissionDeletedTopic));
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