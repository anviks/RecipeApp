using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    public ICollection<AppRefreshToken>? RefreshTokens { get; set; }
    
    public ICollection<Recipe>? AuthoredRecipes { get; set; }
    
    public ICollection<Recipe>? UpdatedRecipes { get; set; }
    
    public ICollection<Review>? Reviews { get; set; }
}