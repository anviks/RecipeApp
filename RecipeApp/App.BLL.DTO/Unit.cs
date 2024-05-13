using Base.Domain;

namespace App.BLL.DTO;

public class Unit : BaseEntityId
{
    public string Name { get; set; } = default!;
    public string? Abbreviation { get; set; }
    public float? UnitMultiplier { get; set; }
    public Guid IngredientTypeId { get; set; }
}