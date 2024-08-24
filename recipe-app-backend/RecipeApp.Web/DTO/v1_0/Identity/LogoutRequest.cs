namespace RecipeApp.Web.DTO.v1_0.Identity;

public class LogoutRequest
{
    public string RefreshToken { get; set; } = default!;
}