namespace App.DTO.v1_0.Identity;

public class LoginRequest
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; } = default!;
}