using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ReimbursementPoC.Administration.API;
using ReimbursementPoC.Administration.API.Mappings;
using ReimbursementPoC.Administration.API.Middleware;
using ReimbursementPoC.Administration.Application;
using ReimbursementPoC.Administration.Infrastructure;
using ReimbursementPoC.Administration.Infrastructure.Health;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(AddSwaggerDocumentation);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(MappingProfile.AutoMapperConfig, typeof(MappingProfile).Assembly);
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new ErrorHandlingFilter());
});
AddHealthChecks(builder);
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

UseSwagger(app);
UseCors(app);
app.MapControllers();
MapHealthCheck(app);

//app.ConfigureEventBus();

app.UseMiddleware<RequestContextLoggingMiddleware>();
app.UseSerilogRequestLogging();   //https://www.milanjovanovic.tech/blog/structured-logging-in-asp-net-core-with-serilog
app.Run();

static void AddHealthChecks(WebApplicationBuilder builder)
{
    var hcBuilder = builder.Services.AddHealthChecks();
    hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy())
             .AddHealthChecks(builder.Configuration);
}

static void AddSwaggerDocumentation(SwaggerGenOptions o)
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
}

static void MapHealthCheck(WebApplication app)
{
    app.MapHealthChecks("/health", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    app.MapHealthChecks("/liveness", new HealthCheckOptions
    {
        Predicate = r => r.Name.Contains("self")
    });
}

static void UseSwagger(WebApplication app)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

static void UseCors(WebApplication app)
{
    app.UseCors(config =>
    {
        config.AllowAnyOrigin();
        config.AllowAnyMethod();
        config.AllowAnyHeader();
    });
}

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddJwtBearer(options =>
//                {
//                    options.Authority = "https://localhost:5000";
//                    options.Audience = "apiscope";
//                    options.TokenValidationParameters = new TokenValidationParameters()
//                    {
//                        NameClaimType = "name"
//                    };
//                });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle