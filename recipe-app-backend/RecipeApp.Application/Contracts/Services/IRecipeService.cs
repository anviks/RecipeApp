using RecipeApp.Application.DTO;
using RecipeApp.Base.Contracts.Infrastructure.Data;

namespace RecipeApp.Application.Contracts.Services;

public interface IRecipeService : IEntityRepository<RecipeResponse>
{
    public Task<RecipeResponse> AddAsync(RecipeRequest recipeRequest, Guid userId, string localWebRootPath);
    public Task<RecipeResponse> UpdateAsync(RecipeRequest recipeRequest, Guid userId, string localWebRootPath);
    public Task<int> RemoveAsync(Guid id, string localWebRootPath);
}