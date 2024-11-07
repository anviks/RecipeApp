using RecipeApp.Base.Contracts.Infrastructure.Data;
using RecipeApp.Infrastructure.Data.DTO;

namespace RecipeApp.Infrastructure.Contracts.Data.Repositories;

public interface IRecipeRepository : IEntityRepository<Recipe>
{
    Task<Recipe?> GetByIdDetailedAsync(Guid id);
    Task<IEnumerable<Recipe>> GetAllDetailedAsync();
}