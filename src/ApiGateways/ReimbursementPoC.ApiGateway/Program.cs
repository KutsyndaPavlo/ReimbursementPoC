using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.Authorization;
using Ocelot.DependencyInjection;
using Ocelot.DownstreamRouteFinder.UrlMatcher;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using Ocelot.Responses;
using ReimbursementPoC.ApiGateway;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer("Bearer",  options =>
               
               {
                   options.Authority = builder.Configuration.GetSection("IdentityAPI").Value;
                   options.Audience = "apiscope";
                   options.TokenValidationParameters = new TokenValidationParameters()
                   {
                       NameClaimType = "name"
                   };
               });

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ErrorHandlingFilter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
var hcBuilder = builder.Services.AddHealthChecks();
hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

//builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
//builder.Services.AddOcelot(builder.Configuration)
//    .AddCacheManager(x =>
//    {
//        x.WithDictionaryHandle();
//    });

builder.Configuration.AddOcelotWithSwaggerSupport(options =>
{
    options.Folder = "Routes";
});

builder.Services.AddOcelot(builder.Configuration).AddPolly();

builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(Path.Combine("Routes", "ocelot.global.json"))
    .AddEnvironmentVariables();

builder.Services.ConfigureDownstreamHostAndPortsPlaceholders(builder.Configuration);
builder.Services.DecorateClaimAuthoriser();

var app = builder.Build();

app.UseMiddleware<UpdateConfigMiddleware>(builder.Configuration);

app.UseSwagger();

app.UseHttpsRedirection();
app.UseCors(config =>
{
    config.AllowAnyOrigin();
    config.AllowAnyMethod();
    config.AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<SetGuidMiddleware>();
app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
    options.ReConfigureUpstreamSwaggerJson = AlterUpstream.AlterUpstreamSwaggerJson;

}).UseOcelot().Wait();

app.MapControllers();
//app.UseOcelot();
app.Run();

public class UpdateConfigMiddleware
{
    private readonly RequestDelegate _next;

    IConfiguration _configuration;

    public UpdateConfigMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context)
    {
        var path = Path.Combine("Routes", "ocelot.SwaggerEndPoints.json");
        var value = _configuration.GetSection("GlobalConfiguration:BaseUrl").Value;

        File.WriteAllText(path, File.ReadAllText(path).Replace("{BaseUrl}", value));

        await _next(context);
    }
}

public class SetGuidMiddleware
{
    private readonly RequestDelegate _next;

    public SetGuidMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var userId = context.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        if (!string.IsNullOrWhiteSpace(userId))
        {
            context.Request.Headers.Add("X-UserId", userId);
        }

        await _next(context);
    }
}

public class ClaimAuthorizerDecorator : IClaimsAuthorizer
{
    private readonly ClaimsAuthorizer _authoriser;

    public ClaimAuthorizerDecorator(ClaimsAuthorizer authoriser)
    {
        _authoriser = authoriser;
    }

    public Response<bool> Authorize(ClaimsPrincipal claimsPrincipal,
                                    Dictionary<string, string> routeClaimsRequirement,
                                    List<PlaceholderNameAndValue> urlPathPlaceholderNameAndValues)
    {
        var newRouteClaimsRequirement = new Dictionary<string, string>();
        foreach (var kvp in routeClaimsRequirement)
        {
            if (kvp.Key.StartsWith("http///"))
            {
                var key = kvp.Key.Replace("http///", "http://");
                newRouteClaimsRequirement.Add(key, kvp.Value);
            }
            else
            {
                newRouteClaimsRequirement.Add(kvp.Key, kvp.Value);
            }
        }

        return _authoriser.Authorize(claimsPrincipal, newRouteClaimsRequirement, urlPathPlaceholderNameAndValues);
    }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection DecorateClaimAuthoriser(this IServiceCollection services)
    {
        var serviceDescriptor = services.First(x => x.ServiceType == typeof(IClaimsAuthorizer));
        services.Remove(serviceDescriptor);

        var newServiceDescriptor = new ServiceDescriptor(serviceDescriptor.ImplementationType, serviceDescriptor.ImplementationType, serviceDescriptor.Lifetime);
        services.Add(newServiceDescriptor);

        services.AddTransient<IClaimsAuthorizer, ClaimAuthorizerDecorator>();

        return services;
    }
}

