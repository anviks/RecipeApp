using RecipeApp.Base.Contracts.Domain;
using RecipeApp.Base.Contracts.Infrastructure.Data;

namespace RecipeApp.Base.Contracts.Application;


public interface IEntityService<TEntity> : IEntityRepository<TEntity>
    where TEntity : class, IDomainEntityId
{
    
}