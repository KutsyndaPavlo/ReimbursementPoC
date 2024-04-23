using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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


builder.Services.AddMassTransit(busConfigurator =>
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

    busConfigurator.AddConsumer<ProgramUpdatedIntegrationEventConsumer>();
    busConfigurator.AddConsumer<ProgramCanceledIntegrationEventConsumer>();
    busConfigurator.AddConsumer<ServiceUpdatedIntegrationEventConsumer>();
    busConfigurator.AddConsumer<ServiceCanceledIntegrationEventConsumer>();
    busConfigurator.AddConsumer<VendorSubmissionCreatedIntegrationEventConsumer>();
    busConfigurator.AddConsumer<VendorSubmissionCanceledIntegrationEventConsumer>();
    busConfigurator.AddConsumer<VendorSubmissionDeletedIntegrationEventConsumer>();
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
