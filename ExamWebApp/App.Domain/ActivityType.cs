using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class ActivityType : BaseEntityId
{
    [MaxLength(128)]
    public string ActivityTypeName { get; set; } = default!;
    
    public ICollection<Activity>? Activities { get; set; }
}