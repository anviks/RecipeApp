using Base.Contracts.Domain;

namespace Base.Contracts.DAL;

public interface IEntityRepository<TEntity> : IEntityRepository<TEntity, Guid>
    where TEntity : class, IDomainEntityId
{
}

public interface IEntityRepository<TEntity, in TKey>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    TEntity Add(TEntity entity);
    Task<TEntity> AddAsync(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    
    TEntity Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    
    TEntity Remove(TEntity entity);
    Task<TEntity> RemoveAsync(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    Task RemoveRangeAsync(IEnumerable<TEntity> entities);
    
    TEntity? Find(TKey id);
    Task<TEntity?> FindAsync(TKey id);
    IEnumerable<TEntity> FindAll();
    Task<IEnumerable<TEntity>> FindAllAsync();
    
    bool Exists(TKey id);
    Task<bool> ExistsAsync(TKey id);
}