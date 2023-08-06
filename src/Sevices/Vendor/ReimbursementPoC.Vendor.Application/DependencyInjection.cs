using FluentValidation;
using MediatR;
using MediatR.Registration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReimbursementPoC.Administration.IntergrationEvents;
using ReimbursementPoC.Infrustructure.EventBus;
using ReimbursementPoC.Infrustructure.EventBus.Abstractions;
using ReimbursementPoC.Infrustructure.EventBusServiceBus;
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
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddEventBus(configuration);
            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var cs = configuration.GetConnectionString("EventBus");

            services.AddSingleton<IServiceBusPersisterConnection>(sp =>
            {
                return new DefaultServiceBusPersisterConnection(cs);
            });

            services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
            {
                var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                string subscriptionName = configuration.GetSection("EventBus:SubscriptionClientName").Value;

                return new EventBusServiceBus(serviceBusPersisterConnection, logger,
                    eventBusSubscriptionsManager, sp, subscriptionName);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            services.AddTransient<ProgramCreatedIntegrationEventHandler>();

            return services;
        }

        public static void ConfigureEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<ProgramCreatedIntegrationEvent, ProgramCreatedIntegrationEventHandler>();
        }
    }
}