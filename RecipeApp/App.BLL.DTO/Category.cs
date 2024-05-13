using Base.Domain;

namespace App.BLL.DTO;

public class Category : BaseEntityId
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}