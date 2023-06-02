﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReimbursementPoC.Vendor.Application.Common.Behaviours;
using ReimbursementPoC.Vendor.Application.Common.Mappings;
using System.Reflection;

namespace ReimbursementPoC.Vendor.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(MappingProfile.AutoMapperConfig, typeof(MappingProfile).Assembly);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            //RegisterEventBus(services);


            return services;
        }

        //private static void RegisterEventBus(IServiceCollection services)
        //{
        //    services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
        //    {
        //        var subscriptionClientName = "catalogApi";//Configuration["SubscriptionClientName"];
        //        var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
        //        ILifetimeScope iLifetimeScope = null;//sp.GetRequiredService<ILifetimeScope>();
        //        var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
        //        var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

        //        var retryCount = 5;
        //        //if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
        //        //{
        //        //    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
        //        //}

        //        return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
        //    });

        //     services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        //    services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        //    {
        //        var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

        //        var factory = new ConnectionFactory()
        //        {
        //            HostName = Environment.GetEnvironmentVariable("EventBusConnection"),
        //            DispatchConsumersAsync = true
        //        };

        //        //if (!string.IsNullOrEmpty(Configuration["EventBusUserName"]))
        //        //{
        //        factory.UserName = Environment.GetEnvironmentVariable("EventBusUserName");// Configuration["EventBusUserName"];
        //        //}

        //        //if (!string.IsNullOrEmpty(Configuration["EventBusPassword"]))
        //        //{
        //            factory.Password = Environment.GetEnvironmentVariable("EventBusPassword");// Configuration["EventBusPassword"];
        //        //}

        //        var retryCount = 5;
        //        //if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
        //        //{
        //        //    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
        //        //}

        //        return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
        //    });

        //    //services.AddTransient<ProductPriceChangedIntegrationEventHandler>();
        //    //services.AddTransient<OrderStartedIntegrationEventHandler>();
        //}

        ////private void ConfigureEventBus(IApplicationBuilder app)
        ////{
        ////    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

        ////    eventBus.Subscribe<ProductPriceChangedIntegrationEvent, ProductPriceChangedIntegrationEventHandler>();
        ////    eventBus.Subscribe<OrderStartedIntegrationEvent, OrderStartedIntegrationEventHandler>();
        ////}


    }
}