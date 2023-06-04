using Microsoft.Extensions.Diagnostics.HealthChecks;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using ReimbursementPoC.ApiGateway;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
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

var routes = "Routes";

builder.Configuration.AddOcelotWithSwaggerSupport(options =>
{
    options.Folder = routes;
});

builder.Services.AddOcelot(builder.Configuration).AddPolly();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddOcelot(routes, builder.Environment)
    .AddEnvironmentVariables();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   // app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
    options.ReConfigureUpstreamSwaggerJson = AlterUpstream.AlterUpstreamSwaggerJson;

}).UseOcelot().Wait();

app.MapControllers();

//app.UseOcelot();

app.Run();
