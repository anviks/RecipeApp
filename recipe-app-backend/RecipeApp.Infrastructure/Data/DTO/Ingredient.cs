using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Infrastructure.Data.DTO;

public class Ingredient : BaseEntityId
{
    public string Name { get; set; } = default!;
    public ICollection<IngredientTypeAssociation>? IngredientTypeAssociations { get; set; }
}