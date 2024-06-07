using Base.Domain;

namespace App.DAL.DTO;

public class ActivityType : BaseEntityId
{
    public string ActivityTypeName { get; set; } = default!;
}