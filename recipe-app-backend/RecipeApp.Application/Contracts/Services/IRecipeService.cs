using RecipeApp.Application.DTO;

namespace RecipeApp.Application.Contracts.Services;

public interface IRecipeService
{
    Task<RecipeResponse?> GetByIdAsync(Guid id, bool tracking = false);
    Task<RecipeResponse?> GetByIdDetailedAsync(Guid id);
    Task<IEnumerable<RecipeResponse>> GetAllAsync(bool tracking = false);
    Task<IEnumerable<RecipeResponse>> GetAllDetailedAsync();
    Task<RecipeResponse> AddAsync(RecipeRequest entity, Guid userId, string localWebRootPath);
    Task<RecipeResponse> UpdateAsync(RecipeRequest entity, Guid userId, string localWebRootPath);
    Task DeleteAsync(RecipeResponse entity, string localWebRootPath);
    Task<bool> ExistsAsync(Guid id);
}