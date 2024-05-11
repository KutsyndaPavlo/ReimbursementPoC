using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ReimbursementPoC.Administration.IntergrationEvents;
using ReimbursementPoC.Vendor.API;
using ReimbursementPoC.Vendor.IntergrationEvents;
using ReimbursementPoC.VendorSearch.API.IntegrationEventHandlers.Program;
using ReimbursementPoC.VendorSearch.API.IntegrationEventHandlers.Service;
using ReimbursementPoC.VendorSearch.API.IntegrationEventHandlers.VendorSubmission;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options =>
{
    options.Filters.Add(new ErrorHandlingFilter());
});



var asb_connection_strig = Environment.GetEnvironmentVariable("ASB_Connection_String");
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddServiceBusMessageScheduler();
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<ProgramCanceledIntegrationEventConsumer>(typeof(ProgramCanceledIntegrationEventConsumerDefinition));
    busConfigurator.AddConsumer<ProgramUpdatedIntegrationEventConsumer>(typeof(ProgramUpdatedIntegrationEventConsumerDefinition));
    busConfigurator.AddConsumer<ServiceCanceledIntegrationEventConsumer>(typeof(ServiceCanceledIntegrationEventConsumerDefinition));
    busConfigurator.AddConsumer<ServiceUpdatedIntegrationEventConsumer>(typeof(ServiceUpdatedIntegrationEventConsumerDefinition));
    busConfigurator.AddConsumer<VendorSubmissionCanceledIntegrationEventConsumer>(typeof(VendorSubmissionCanceledIntegrationEventConsumerDefinition));
    busConfigurator.AddConsumer<VendorSubmissionCreatedIntegrationEventConsumer>(typeof(VendorSubmissionCreatedIntegrationEventConsumerDefinition));
    busConfigurator.AddConsumer<VendorSubmissionDeletedIntegrationEventConsumer>(typeof(VendorSubmissionDeletedIntegrationEventConsumerDefinition));

    if (!string.IsNullOrWhiteSpace(asb_connection_strig))
    {
        busConfigurator.UsingAzureServiceBus((context, configurator) =>
        {
            configurator.Host(asb_connection_strig);

            configurator.ConfigureEndpoints(context);
            // https://tech.playgokids.com/messaging-masstransit-azure-servicebus-net7/

            configurator.UseServiceBusMessageScheduler();
            configurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));

            configurator.SubscriptionEndpoint<ProgramCanceledIntegrationEvent>(ReimbursementPoC.Administration.IntergrationEvents.Constants.ProgramCanceledSubscription, configurator =>
            {
                configurator.ConfigureConsumer<ProgramCanceledIntegrationEventConsumer>(context);
            });
            configurator.SubscriptionEndpoint<ProgramUpdatedIntegrationEvent>(ReimbursementPoC.Administration.IntergrationEvents.Constants.ProgramUpdatedSubscription, configurator =>
            {
                configurator.ConfigureConsumer<ProgramUpdatedIntegrationEventConsumer>(context);
            });

            configurator.SubscriptionEndpoint<ServiceCanceledIntegrationEvent>(ReimbursementPoC.Administration.IntergrationEvents.Constants.ServiceCanceledSubscription, configurator =>
            {
                configurator.ConfigureConsumer<ServiceCanceledIntegrationEventConsumer>(context);
            });
            configurator.SubscriptionEndpoint<ServiceUpdatedIntegrationEvent>(ReimbursementPoC.Administration.IntergrationEvents.Constants.ServiceUpdatedSubscription, configurator =>
            {
                configurator.ConfigureConsumer<ServiceUpdatedIntegrationEventConsumer>(context);
            });

            configurator.SubscriptionEndpoint<VendorSubmissionCanceledIntegrationEvent>(ReimbursementPoC.Vendor.IntergrationEvents.Constants.VendorSubmissionCanceledSubscription, configurator =>
            {
                configurator.ConfigureConsumer<VendorSubmissionCanceledIntegrationEventConsumer>(context);
            });            
            configurator.SubscriptionEndpoint<VendorSubmissionCreatedIntegrationEvent>(ReimbursementPoC.Vendor.IntergrationEvents.Constants.VendorSubmissionCreatedSubscription, configurator =>
            {
                configurator.ConfigureConsumer<VendorSubmissionCreatedIntegrationEventConsumer>(context);
            });
            configurator.SubscriptionEndpoint<VendorSubmissionDeletedIntegrationEvent>(ReimbursementPoC.Vendor.IntergrationEvents.Constants.VendorSubmissionDeletedSubscription, configurator =>
            {
                configurator.ConfigureConsumer<VendorSubmissionDeletedIntegrationEventConsumer>(context);
            });
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

var hcBuilder = builder.Services.AddHealthChecks();
hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecks("/liveness", new HealthCheckOptions
{
    Predicate = r => r.Name.Contains("self")
});

app.Run();
