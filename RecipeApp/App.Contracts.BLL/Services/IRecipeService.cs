using Base.Contracts.DAL;
using BLL_DTO = App.BLL.DTO;

namespace App.Contracts.BLL.Services;

public interface IRecipeService : IEntityRepository<BLL_DTO.RecipeResponse>
{
    public Task<BLL_DTO.RecipeResponse> AddAsync(BLL_DTO.RecipeRequest recipeRequest, Guid userId, string localWebRootPath);
    public Task<BLL_DTO.RecipeResponse> UpdateAsync(BLL_DTO.RecipeRequest recipeRequest, Guid userId, string localWebRootPath);
    public Task<int> RemoveAsync(Guid id, string localWebRootPath);
}