using Base.Domain;

namespace App.DTO.v1_0;

public class Category : BaseEntityId
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}