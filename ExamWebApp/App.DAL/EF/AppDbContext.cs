using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<AppRefreshToken> RefreshTokens { get; set; } = default!;

    public DbSet<Sample> Samples { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        SetUniversalTime();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        SetUniversalTime();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetUniversalTime();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    private void SetUniversalTime()
    {
        foreach (EntityEntry entity in ChangeTracker.Entries().Where(e => e.State != EntityState.Deleted))
        {
            foreach (PropertyEntry prop in entity
                         .Properties
                         .Where(x => x.Metadata.ClrType == typeof(DateTime) || x.Metadata.ClrType == typeof(DateTime?)))
            {
                Console.WriteLine(prop.Metadata.Name);
                if (prop.CurrentValue != null)
                {
                    prop.CurrentValue = ((DateTime)prop.CurrentValue).ToUniversalTime();
                }
            }
        }
    }
}