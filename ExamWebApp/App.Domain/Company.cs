using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Company : BaseEntityId
{
    [MaxLength(128)]
    public string CompanyName { get; set; } = default!;
    
    public ICollection<AppUser>? Users { get; set; }
    public ICollection<Raffle>? Raffles { get; set; }
}