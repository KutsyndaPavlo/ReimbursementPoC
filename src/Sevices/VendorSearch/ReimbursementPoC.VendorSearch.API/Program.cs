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



var asb_connection_strig = Environment.GetEnvironmentVariable("ASB_Connection_String");
builder.Services.AddMassTransit(busConfigurator =>
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
