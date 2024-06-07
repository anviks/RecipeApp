using Base.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; }
    
    public ICollection<AppRefreshToken>? RefreshTokens { get; set; }
    public ICollection<Ticket>? Tickets { get; set; }
    public ICollection<Activity>? Activities { get; set; }
    public ICollection<RaffleResult>? RaffleResults { get; set; }
}