using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Entities.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    public ICollection<AppRefreshToken>? RefreshTokens { get; set; }
    
    public ICollection<Recipe>? AuthoredRecipes { get; set; }
    
    public ICollection<Recipe>? UpdatedRecipes { get; set; }
    
    public ICollection<Review>? Reviews { get; set; }
}