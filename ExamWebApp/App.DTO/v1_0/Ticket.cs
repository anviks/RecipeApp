using Base.Domain;

namespace App.DTO.v1_0;

public class Ticket : BaseEntityId
{
    public Guid UserId { get; set; }
    public Guid RaffleId { get; set; }
}