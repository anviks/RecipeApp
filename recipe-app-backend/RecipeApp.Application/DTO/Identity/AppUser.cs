using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Application.DTO.Identity;

public class AppUser : BaseEntityId
{
    public string Username { get; set; } = default!;
}