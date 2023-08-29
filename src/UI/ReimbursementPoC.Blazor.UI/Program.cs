   using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ReimbursementPoC.Blazor.UI;
using ReimbursementPoC.Blazor.UI.Security;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

Console.WriteLine($"HostEnvironment: {builder.HostEnvironment}");
Console.WriteLine($"GatewayApi: {builder.Configuration.GetSection("GatewayApi").Value}");

builder.Services.AddHttpClient("api", client =>
                {
                    client.BaseAddress = new Uri(builder.Configuration.GetSection("GatewayApi").Value);
                })
                .AddHttpMessageHandler(sp =>
                {
                    var handler = sp.GetService<AuthorizationMessageHandler>()
                        .ConfigureHandler(
                            authorizedUrls: new[] { builder.Configuration.GetSection("GatewayApi").Value },
                            scopes: new[] { "apiscope" });

                    return handler;
                });

builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("api"));

builder.Services
    .AddOidcAuthentication(options =>
    {
        builder.Configuration.Bind("oidc", options.ProviderOptions);
        options.UserOptions.RoleClaim = "role";
    })
    .AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

await builder.Build().RunAsync();
