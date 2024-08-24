using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Web.DTO.v1_0;

public class IngredientTypeAssociation : BaseEntityId
{
    public Guid IngredientId { get; set; }
    public Guid IngredientTypeId { get; set; }
}