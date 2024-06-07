using Base.Domain;

namespace App.DTO.v1_0;

public class ActivityType : BaseEntityId
{
    public string ActivityTypeName { get; set; } = default!;
}