using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WOSRSTest.Server;
using WOSRSTest.Server.Data;
using WOSRSTest.Server.Models;
using static OpenIddict.Abstractions.OpenIddictConstants;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    Args = args,
});

string port = builder.Configuration["PORT"];

if (!string.IsNullOrEmpty(port))
{
    builder = WebApplication.CreateBuilder(new WebApplicationOptions()
    {
        Args = args,
        ContentRootPath = "/app/out",
        WebRootPath = "wwwroot",
    });
    builder.WebHost.UseUrls($"http://*:{port}");
}

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    // logging.AddEventLog();
    logging.AddConsole();
    logging.AddDebug();
    logging.AddEventSourceLogger();
});

bool isDev = builder.Environment.IsDevelopment();

string connectionString = builder.Configuration["ConnectionStrings:TestDb"];

if (string.IsNullOrEmpty(connectionString))
{
    connectionString = builder.Configuration["TestDb"];
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString, o => o.SetPostgresVersion(new System.Version(9, 6)));
    options.UseOpenIddict();
});

builder.Services.AddDbContext<DataDbContext>(options => options.UseNpgsql(connectionString, o => o.SetPostgresVersion(new System.Version(9, 6))));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

builder.Services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>();
                })
                .AddServer(options =>
                {
                    // Enable the authorization, logout, token and userinfo endpoints.
                    options.SetAuthorizationEndpointUris("/connect/authorize")
                           .SetLogoutEndpointUris("/connect/logout")
                           .SetTokenEndpointUris("/connect/token")
                           .SetUserinfoEndpointUris("/connect/userinfo");

                    // Mark the "email", "profile" and "roles" scopes as supported scopes.
                    options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);

                    // Note: the sample uses the code and refresh token flows but you can enable
                    // the other flows if you need to support implicit, password or client credentials.
                    options.AllowAuthorizationCodeFlow()
                           .AllowRefreshTokenFlow();

                    // Register the signing and encryption credentials.
                    options.AddEphemeralEncryptionKey()
                           .AddEphemeralSigningKey();
                    //options.AddEncryptionCertificate(certificateWithPrivateKey)
                    //       .AddSigningCertificate(certificateWithPrivateKey);

                    // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                    options.UseAspNetCore()
                           .EnableAuthorizationEndpointPassthrough()
                           .EnableLogoutEndpointPassthrough()
                           .EnableStatusCodePagesIntegration()
                           .EnableTokenEndpointPassthrough();
                })
                .AddValidation(options =>
                {
                    options.UseLocalServer();

                    options.UseAspNetCore();
                });

//builder.Services.AddAuthentication();
//builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.ClaimsIdentity.UserNameClaimType = Claims.Name;
    options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
    options.ClaimsIdentity.RoleClaimType = Claims.Role;

    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.AddHostedService<Worker>();

WebApplication app = builder.Build();
app.Use((context, next) =>
{
    context.Request.Scheme = "https";
    return next();
});
app.UseResponseCompression();
var forwardedHeaderOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
};
forwardedHeaderOptions.KnownProxies.Add(IPAddress.Parse("192.168.1.20")); // reverse proxy IP
app.UseForwardedHeaders(forwardedHeaderOptions);
app.UseCors(cors => cors
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true)
.AllowCredentials()
);

// Configure the HTTP request pipeline.
if (isDev)
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html");
});

app.Run();
