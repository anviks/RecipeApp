using Base.Contracts.DAL;
using DAL_DTO = App.DAL.DTO;

namespace App.Contracts.DAL.Repositories;

public interface IRecipeIngredientRepository : IEntityRepository<DAL_DTO.RecipeIngredient>
{
    
}