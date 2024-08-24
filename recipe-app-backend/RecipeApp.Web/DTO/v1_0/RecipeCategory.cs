using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Web.DTO.v1_0;

public class RecipeCategory : BaseEntityId
{
    public Guid CategoryId { get; set; }
    public Guid RecipeId { get; set; }
}