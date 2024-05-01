using BLL_DTO = App.BLL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IRecipeService : IEntityRepository<BLL_DTO.Recipe>
{
    
}