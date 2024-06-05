using Base.Domain;

namespace App.Domain.Identity;

public class AppRefreshToken : BaseRefreshToken
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}