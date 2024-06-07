using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Prize : BaseEntityId
{
    [MaxLength(128)]
    public string PrizeName { get; set; } = default!;
    
    public Guid? RaffleResultId { get; set; }
    public RaffleResult? RaffleResult { get; set; }
    
    public Guid RaffleId { get; set; }
    public Raffle? Raffle { get; set; }
}