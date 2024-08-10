using Base.Contracts.DAL;
using DAL_DTO = App.DAL.DTO;

namespace App.Contracts.DAL.Repositories;

public interface IRecipeCategoryRepository : IEntityRepository<DAL_DTO.RecipeCategory>
{
    
}