using Base.Contracts.Domain;
using RecipeApp.Base.Contracts.Infrastructure.Data;

namespace RecipeApp.Base.Contracts.Application;

public interface IEntityService<TEntity> : IEntityRepository<TEntity>, IEntityService<TEntity, Guid>
    where TEntity : class, IDomainEntityId
{
    
}

public interface IEntityService<TEntity, in TKey> : IEntityRepository<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey> 
    where TKey : IEquatable<TKey>
{
    
}