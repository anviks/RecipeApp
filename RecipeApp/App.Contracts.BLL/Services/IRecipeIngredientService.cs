using Base.Contracts.DAL;
using BLL_DTO = App.BLL.DTO;

namespace App.Contracts.BLL.Services;

public interface IRecipeIngredientService : IEntityRepository<BLL_DTO.RecipeIngredient>
{
    
}