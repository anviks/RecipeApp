using Base.Domain;

namespace App.DTO.v1_0.Identity;

public class AppUser : BaseEntityId
{
    public string Username { get; set; } = default!;
}