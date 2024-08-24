using RecipeApp.Base.Contracts.Infrastructure.Data;
using RecipeApp.Infrastructure.Data.DTO;

namespace RecipeApp.Infrastructure.Contracts.Data.Repositories;

public interface IRecipeRepository : IEntityRepository<Recipe>
{
    
}