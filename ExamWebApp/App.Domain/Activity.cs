using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Activity : BaseEntityId
{
    public TimeSpan Duration { get; set; }
    public DateTime Date { get; set; }
    
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    
    public Guid ActivityTypeId { get; set; }
    public ActivityType? ActivityType { get; set; }
}