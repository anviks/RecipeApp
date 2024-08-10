using Base.Contracts.DAL;
using BLL_DTO = App.BLL.DTO;

namespace App.Contracts.BLL.Services;

public interface IRecipeCategoryService : IEntityRepository<BLL_DTO.RecipeCategory>
{
    
}