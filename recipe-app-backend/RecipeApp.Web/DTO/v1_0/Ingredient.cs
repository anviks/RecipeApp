using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Web.DTO.v1_0;

public class Ingredient : BaseEntityId
{
    public string Name { get; set; } = default!;
    public ICollection<IngredientTypeAssociation>? IngredientTypeAssociations { get; set; }
}