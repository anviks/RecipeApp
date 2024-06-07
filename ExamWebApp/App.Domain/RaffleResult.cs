using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class RaffleResult : BaseEntityId
{
    public Prize? Prize { get; set; }
    
    public Guid RaffleId { get; set; }
    public Raffle? Raffle { get; set; }
    
    public Guid? UserId { get; set; }
    public AppUser? User { get; set; }
    [MaxLength(64)]
    public string? AnonymousUserName { get; set; }
}