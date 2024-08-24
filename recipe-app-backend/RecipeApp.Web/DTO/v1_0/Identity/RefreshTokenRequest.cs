namespace RecipeApp.Web.DTO.v1_0.Identity;

public class RefreshTokenRequest
{
    public string? JsonWebToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}