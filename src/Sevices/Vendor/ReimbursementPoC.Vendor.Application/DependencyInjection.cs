using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReimbursementPoC.Vendor.Application.Common.Behaviours;
using ReimbursementPoC.Vendor.Application.Common.Mappings;
using ReimbursementPoC.Vendor.Application.VendorSubmission.DomainServices;
using ReimbursementPoC.Vendor.Application.VendorSubmission.IntegrationEvents;
using ReimbursementPoC.Vendor.Domain.VendorSubmission.DomainServices;
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
            var cs = configuration.GetConnectionString("EventBus");

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                //busConfigurator.UsingAzureServiceBus((context, configurator) =>
                //{
                //    configurator.Host(cs);

                //    configurator.ConfigureEndpoints(context);
                //});

                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(Environment.GetEnvironmentVariable("RabbitMqHost") ?? "localhost", "/", h =>
                    {
                        h.Username(Environment.GetEnvironmentVariable("RabbitMqUser"));
                        h.Password(Environment.GetEnvironmentVariable("RabbitMqPass"));
                    });

                    configurator.ConfigureEndpoints(context);
                });

                busConfigurator.AddConsumer<ProgramCreatedIntegrationEventConsumer>();
            });

            return services;
        }
    }
}