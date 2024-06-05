using Base.Domain.Contracts;

namespace Base.DAL.Contracts;

public interface IEntityRepository<TEntity> : IEntityRepository<TEntity, Guid>
    where TEntity : class, IDomainEntityId
{
}

public interface IEntityRepository<TEntity, in TKey>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    TEntity Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    
    TEntity Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    
    int Remove(TEntity entity);
    int Remove(TKey id);
    Task<int> RemoveAsync(TEntity entity);
    Task<int> RemoveAsync(TKey id);
    int RemoveRange(IEnumerable<TEntity> entities);
    int RemoveRange(IEnumerable<TKey> ids);
    
    TEntity? Find(TKey id, bool tracking = false);
    Task<TEntity?> FindAsync(TKey id, bool tracking = false);
    IEnumerable<TEntity> FindAll(bool tracking = false);
    Task<IEnumerable<TEntity>> FindAllAsync(bool tracking = false);
    
    bool Exists(TKey id, bool tracking = false);
    Task<bool> ExistsAsync(TKey id, bool tracking = false);
}