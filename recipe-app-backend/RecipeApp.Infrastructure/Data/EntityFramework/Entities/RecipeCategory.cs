using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Entities;

public class RecipeCategory : BaseEntityId
{
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public Guid RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
}