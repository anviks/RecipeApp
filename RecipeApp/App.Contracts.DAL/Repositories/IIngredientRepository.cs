using DAL_DTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IIngredientRepository : IEntityRepository<DAL_DTO.Ingredient>
{
}