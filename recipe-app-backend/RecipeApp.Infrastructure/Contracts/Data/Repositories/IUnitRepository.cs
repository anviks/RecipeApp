using RecipeApp.Base.Contracts.Infrastructure.Data;
using Unit = RecipeApp.Infrastructure.Data.DTO.Unit;

namespace RecipeApp.Infrastructure.Contracts.Data.Repositories;

public interface IUnitRepository : IEntityRepository<Unit>
{
    
}