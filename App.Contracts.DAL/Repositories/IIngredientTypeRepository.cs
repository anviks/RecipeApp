using DAL_DTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IIngredientTypeRepository : IEntityRepository<DAL_DTO.IngredientType>
{
    
}