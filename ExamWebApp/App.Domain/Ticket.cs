using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Ticket : BaseEntityId
{
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    
    public Guid RaffleId { get; set; }
    public Raffle? Raffle { get; set; }
}