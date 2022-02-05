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

var baseUri = builder.HostEnvironment.BaseAddress;

//builder.Services.AddHttpClient("WOSRS.ServerAPI", client => client.BaseAddress = baseUri)
builder.Services.AddHttpClient("WOSRS.ServerAPI")
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(baseUri))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(provider =>
{
    var factory = provider.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("WOSRS.ServerAPI");
});

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.ClientId = "WOSRS.Client";
    options.ProviderOptions.Authority = baseUri;
    options.ProviderOptions.ResponseType = "code";
    options.AuthenticationPaths.LogInCallbackPath = $"{baseUri}authentication/login-callback";
    options.AuthenticationPaths.LogOutCallbackPath = $"{baseUri}authentication/logout-callback";
    options.ProviderOptions.RedirectUri = $"{baseUri}authentication/login-callback";
    options.ProviderOptions.ResponseMode = "query";
    options.AuthenticationPaths.RemoteRegisterPath = $"{baseUri}Identity/Account/Register";
});

//builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
