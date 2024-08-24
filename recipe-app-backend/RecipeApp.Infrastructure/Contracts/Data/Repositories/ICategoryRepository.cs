using RecipeApp.Base.Contracts.Infrastructure.Data;
using DAL_DTO = RecipeApp.Infrastructure.Data.DTO;

namespace RecipeApp.Infrastructure.Contracts.Data.Repositories;

public interface ICategoryRepository : IEntityRepository<DAL_DTO.Category>
{
    
}