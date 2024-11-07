using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Infrastructure.Data.DTO;

public class RecipeIngredient : BaseEntityId
{
    public string? CustomUnit { get; set; }
    public float Quantity { get; set; }
    public string? IngredientModifier { get; set; } = default!;
    public Guid? UnitId { get; set; }
    public Unit? Unit { get; set; }
    
    public Guid RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
    
    public Guid IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
}