using Base.Domain;

namespace App.DTO.v1_0;

public class Activity : BaseEntityId
{
    public int DurationInMinutes { get; set; }
    public DateTime Date { get; set; }
    public Guid UserId { get; set; }
    public Guid ActivityTypeId { get; set; }
}