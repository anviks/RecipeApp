using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1_0.Identity;

public class RegisterRequest
{
    [Required]
    [EmailAddress] 
    public string Email { get; set; } = default!;

    [Required]
    [StringLength(64, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
    [RegularExpression("^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")]
    public string Username { get; set; } = default!;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    public string Password { get; set; } = default!;
}