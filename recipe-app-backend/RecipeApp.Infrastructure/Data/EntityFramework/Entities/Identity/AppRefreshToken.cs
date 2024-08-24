using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Entities.Identity;

public class AppRefreshToken : BaseRefreshToken
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}