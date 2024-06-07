using Base.Domain;

namespace App.DTO.v1_0.Identity;

public class AppUser : BaseEntityId
{
    public Guid? CompanyId { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
}