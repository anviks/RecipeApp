namespace App.DTO.v1_0.Identity;

public class LoginRequest
{
    public string UsernameOrEmail { get; set; } = default!;
    public string Password { get; set; } = default!;
}