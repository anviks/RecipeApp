using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        builder.Entity<Recipe>()
            .HasOne(r => r.AuthorUser)
            .WithMany(u => u.AuthoredRecipes)
            .HasForeignKey(r => r.AuthorUserId);

        builder.Entity<Recipe>()
            .HasOne(r => r.UpdatingUser) // Recipe has one UpdatingUser
            .WithMany(u => u.UpdatedRecipes) // AppUser has many UpdatedRecipes
            .HasForeignKey(r => r.UpdatingUserId);
        
        var volumetricId = Guid.NewGuid();
        var weighableId = Guid.NewGuid();
        var countableId = Guid.NewGuid();
        var miscellaneousId = Guid.NewGuid();
        
        builder.Entity<IngredientType>()
            .HasData(
                new IngredientType { Id = volumetricId, Name = "Volumetric", Description = "An ingredient that can be measured by volume." },
                new IngredientType { Id = weighableId, Name = "Weighable", Description = "An ingredient that can be measured by weight." },
                new IngredientType { Id = countableId, Name = "Countable", Description = "An ingredient that can be counted." }
            );

        builder.Entity<Unit>()
            .HasData(
                new Unit { Id = Guid.NewGuid(), Name = "gram", Abbreviation = "g", IngredientTypeId = weighableId, UnitMultiplier = 1 },
                new Unit { Id = Guid.NewGuid(), Name = "kilogram", Abbreviation = "kg", IngredientTypeId = weighableId, UnitMultiplier = 1000 },
                new Unit { Id = Guid.NewGuid(), Name = "ounce", Abbreviation = "oz", IngredientTypeId = weighableId, UnitMultiplier = 28.3495f },
                new Unit { Id = Guid.NewGuid(), Name = "pound", Abbreviation = "lb", IngredientTypeId = weighableId, UnitMultiplier = 453.592f },
                
                new Unit { Id = Guid.NewGuid(), Name = "milliliter", Abbreviation = "ml", IngredientTypeId = volumetricId, UnitMultiplier = 1 },
                new Unit { Id = Guid.NewGuid(), Name = "liter", Abbreviation = "l", IngredientTypeId = volumetricId, UnitMultiplier = 1000 },
                new Unit { Id = Guid.NewGuid(), Name = "fluid ounce", Abbreviation = "fl oz", IngredientTypeId = volumetricId, UnitMultiplier = 29.5735f },
                new Unit { Id = Guid.NewGuid(), Name = "cup", Abbreviation = "c", IngredientTypeId = volumetricId, UnitMultiplier = 236.588f },
                new Unit { Id = Guid.NewGuid(), Name = "pint", Abbreviation = "pt", IngredientTypeId = volumetricId, UnitMultiplier = 473.176f },
                new Unit { Id = Guid.NewGuid(), Name = "quart", Abbreviation = "qt", IngredientTypeId = volumetricId, UnitMultiplier = 946.353f },
                new Unit { Id = Guid.NewGuid(), Name = "gallon", Abbreviation = "gal", IngredientTypeId = volumetricId, UnitMultiplier = 3785.41f },
                new Unit { Id = Guid.NewGuid(), Name = "teaspoon", Abbreviation = "tsp", IngredientTypeId = volumetricId, UnitMultiplier = 4.92892f },
                new Unit { Id = Guid.NewGuid(), Name = "tablespoon", Abbreviation = "tbsp", IngredientTypeId = volumetricId, UnitMultiplier = 14.7868f }
            );

        base.OnModelCreating(builder);
    }
}