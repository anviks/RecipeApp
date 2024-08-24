using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Web.DTO.v1_0.Identity;

public class AppUser : BaseEntityId
{
    public string Username { get; set; } = default!;
}