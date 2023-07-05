using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ReimbursementPoC.Blazor.UI;
using ReimbursementPoC.Blazor.UI.Security;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("api")
                .AddHttpMessageHandler(sp =>
                {
                    var handler = sp.GetService<AuthorizationMessageHandler>()
                        .ConfigureHandler(
                            authorizedUrls: new[] { "https://localhost:7120" },
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
