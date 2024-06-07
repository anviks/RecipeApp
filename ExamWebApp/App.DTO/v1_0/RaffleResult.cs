using Base.Domain;

namespace App.DTO.v1_0;

public class RaffleResult : BaseEntityId
{
    public Guid RaffleId { get; set; }
    public Guid? UserId { get; set; }
    public string? AnonymousUserName { get; set; }
}