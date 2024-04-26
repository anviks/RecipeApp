namespace App.DTO.v1_0.Identity;

public class TokenResponse
{
    public string JsonWebToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}