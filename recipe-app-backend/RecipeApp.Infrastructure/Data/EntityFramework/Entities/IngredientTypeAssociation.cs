using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Entities;

public class IngredientTypeAssociation : BaseEntityId
{
    public Guid IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
    
    public Guid IngredientTypeId { get; set; }
    public IngredientType? IngredientType { get; set; }
}