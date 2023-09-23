//using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WOSRSTest.Server.Models;

namespace WOSRSTest.Server.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.Entity<ApplicationUser>().Property(p => p.SettingsId).HasDefaultValueSql(SqlSnippets.GenerateSettings);

        base.OnModelCreating(builder);
    }
}
