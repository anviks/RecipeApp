using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Infrastructure.Data.DTO;

public class IngredientType : BaseEntityId
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}