using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Infrastructure.Data.DTO;

public class RecipeCategory : BaseEntityId
{
    public Guid CategoryId { get; set; }
    public Guid RecipeId { get; set; }
}