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

    // public DbSet<AppRole> AppRoles { get; set; } = default!;
    // public DbSet<AppUser> AppUsers { get; set; } = default!;
    // public DbSet<AppUserRole> AppUserRoles { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Ingredient> Ingredients { get; set; } = default!;
    public DbSet<IngredientType> IngredientTypes { get; set; } = default!;
    public DbSet<IngredientTypeAssociation> IngredientTypeAssociations { get; set; } = default!;
    public DbSet<Recipe> Recipes { get; set; } = default!;
    public DbSet<RecipeCategory> RecipeCategories { get; set; } = default!;
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; } = default!;
    public DbSet<Review> Reviews { get; set; } = default!;
    public DbSet<Unit> Units { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Recipe>()
            .HasOne(r => r.AuthorUser)
            .WithMany(u => u.AuthoredRecipes)
            .HasForeignKey(r => r.AuthorUserId);

        builder.Entity<Recipe>()
            .HasOne(r => r.UpdatingUser) // Recipe has one UpdatingUser
            .WithMany(u => u.UpdatedRecipes) // AppUser has many UpdatedRecipes
            .HasForeignKey(r => r.UpdatingUserId);

        if (Database.ProviderName!.Contains("InMemory"))
        {
            builder.Entity<Category>().OwnsOne(c => c.Name, b => b.ToJson());
            builder.Entity<Category>().OwnsOne(c => c.Description, b => b.ToJson());
            builder.Entity<Ingredient>().OwnsOne(i => i.Name, b => b.ToJson());
            builder.Entity<IngredientType>().OwnsOne(it => it.Name, b => b.ToJson());
            builder.Entity<IngredientType>().OwnsOne(it => it.Description, b => b.ToJson());
            builder.Entity<Recipe>().OwnsOne(r => r.Title, b => b.ToJson());
            builder.Entity<RecipeIngredient>().OwnsOne(ri => ri.IngredientModifier, b => b.ToJson());
            builder.Entity<RecipeIngredient>().OwnsOne(ri => ri.CustomUnit, b => b.ToJson());
            builder.Entity<Unit>().OwnsOne(u => u.Name, b => b.ToJson());
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
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

        return base.SaveChangesAsync(cancellationToken);
    }
}