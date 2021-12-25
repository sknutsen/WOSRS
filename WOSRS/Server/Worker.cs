using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using WOSRS.Server.Data;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace WOSRS.Server
{
    public class Worker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("WOSRS.Client") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "WOSRS.Client",
                    ConsentType = ConsentTypes.Explicit,
                    DisplayName = "WOSRS",
                    Type = ClientTypes.Public,
                    PostLogoutRedirectUris =
                    {
                        new Uri("https://wosrsserver.azurewebsites.net/authentication/logout-callback"),
                        new Uri("https://wosrs.zdk.no/authentication/logout-callback"),
                        new Uri("https://localhost:44371/authentication/logout-callback"),
                    },
                    RedirectUris =
                    {
                        new Uri("https://wosrsserver.azurewebsites.net/authentication/login-callback"),
                        new Uri("https://wosrs.zdk.no/authentication/login-callback"),
                        new Uri("https://localhost:44371/authentication/login-callback"),
                    },
                    Permissions =
                    {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Logout,
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,
                        Permissions.ResponseTypes.Code,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles
                    },
                    Requirements =
                    {
                        Requirements.Features.ProofKeyForCodeExchange
                    }
                });
            }

        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
