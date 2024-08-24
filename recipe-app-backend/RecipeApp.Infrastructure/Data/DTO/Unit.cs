using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Infrastructure.Data.DTO;

public class Unit : BaseEntityId
{
    public string Name { get; set; } = default!;
    public string? Abbreviation { get; set; }
    public float? UnitMultiplier { get; set; }
    public Guid IngredientTypeId { get; set; }
}