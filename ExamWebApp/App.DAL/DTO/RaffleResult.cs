using Base.Domain;

namespace App.DAL.DTO;

public class RaffleResult : BaseEntityId
{
    public Guid RaffleId { get; set; }
    public Guid? UserId { get; set; }
    public string? AnonymousUserName { get; set; }
}