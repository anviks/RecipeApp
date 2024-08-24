using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Web.DTO.v1_0;

public class IngredientType : BaseEntityId
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}