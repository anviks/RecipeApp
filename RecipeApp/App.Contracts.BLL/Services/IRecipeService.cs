using BLL_DTO = App.BLL.DTO;
using Base.Contracts.DAL;
using Microsoft.AspNetCore.Http;

namespace App.Contracts.BLL.Services;

public interface IRecipeService : IEntityRepository<BLL_DTO.RecipeResponse>
{
    public BLL_DTO.RecipeResponse Add(BLL_DTO.RecipeResponse recipeResponse, Guid userId);
    public BLL_DTO.RecipeResponse Add(BLL_DTO.RecipeRequest recipe, Guid userId);
    public BLL_DTO.RecipeResponse Update(BLL_DTO.RecipeResponse recipeResponse, Guid userId);
}