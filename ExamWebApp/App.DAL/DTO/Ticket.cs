using Base.Domain;

namespace App.DAL.DTO;

public class Ticket : BaseEntityId
{
    public Guid UserId { get; set; }
    public Guid RaffleId { get; set; }
}