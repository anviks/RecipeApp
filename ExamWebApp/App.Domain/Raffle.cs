using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Raffle : BaseEntityId
{
    [MaxLength(128)]
    public string RaffleName { get; set; } = default!;
    public bool VisibleToPublic { get; set; }
    public bool AllowAnonymousUsers { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; }
    
    public IEnumerable<Prize>? Prizes { get; set; }
    public IEnumerable<RaffleResult>? RaffleResults { get; set; }
    public IEnumerable<Ticket>? Tickets { get; set; }
}