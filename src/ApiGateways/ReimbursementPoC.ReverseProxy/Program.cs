using ReimbursementPoC.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors(config =>
{
    config.AllowAnyOrigin();
    config.AllowAnyMethod();
    config.AllowAnyHeader();
});

app.UseMiddleware<ReverseProxyMiddleware>();


app.Run();