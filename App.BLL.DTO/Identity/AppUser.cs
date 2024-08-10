using Base.Domain;

namespace App.BLL.DTO.Identity;

public class AppUser : BaseEntityId
{
    public string Username { get; set; } = default!;
}