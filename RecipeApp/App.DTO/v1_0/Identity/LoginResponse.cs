namespace App.DTO.v1_0.Identity;

public class LoginResponse
{
    public string JsonWebToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
}