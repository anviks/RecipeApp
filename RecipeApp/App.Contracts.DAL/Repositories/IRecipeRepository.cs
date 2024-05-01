using Base.Contracts.DAL;
using DAL_DTO = App.DAL.DTO;

namespace App.Contracts.DAL.Repositories;

public interface IRecipeRepository : IEntityRepository<DAL_DTO.Recipe>
{
    
}