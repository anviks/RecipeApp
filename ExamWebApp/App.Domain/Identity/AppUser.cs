using Base.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    public ICollection<AppRefreshToken>? RefreshTokens { get; set; }
}