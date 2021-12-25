using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WOSRS.Client;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

var baseUri = new Uri(builder.HostEnvironment.BaseAddress);

//builder.Services.AddHttpClient("WOSRS.ServerAPI", client => client.BaseAddress = baseUri)
builder.Services.AddHttpClient("WOSRS.ServerAPI")
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
//builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WOSRS.ServerAPI"));
builder.Services.AddScoped(provider =>
{
    var factory = provider.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("WOSRS.ServerAPI");
});

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.ClientId = "WOSRS.Client";
    //options.ProviderOptions.Authority = "https://localhost:44310/";
    options.ProviderOptions.Authority = "https://localhost:44371/";
    options.ProviderOptions.ResponseType = "code";
    options.AuthenticationPaths.LogInCallbackPath = "https://localhost:44371/authentication/login-callback";
    options.AuthenticationPaths.LogOutCallbackPath = "https://localhost:44371/authentication/logout-callback";
    options.ProviderOptions.RedirectUri = "https://localhost:44371/authentication/login-callback";

    // Note: response_mode=fragment is the best option for a SPA. Unfortunately, the Blazor WASM
    // authentication stack is impacted by a bug that prevents it from correctly extracting
    // authorization error responses (e.g error=access_denied responses) from the URL fragment.
    // For more information about this bug, visit https://github.com/dotnet/aspnetcore/issues/28344.
    //
    options.ProviderOptions.ResponseMode = "query";
    options.AuthenticationPaths.RemoteRegisterPath = $"https://localhost:44371/Identity/Account/Register";
});

//builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
