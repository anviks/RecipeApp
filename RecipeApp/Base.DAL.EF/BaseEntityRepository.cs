using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Base.DAL.EF;

public class BaseEntityRepository<TDomainEntity, TDalEntity, TDbContext> :
    BaseEntityRepository<Guid, TDomainEntity, TDalEntity, TDbContext>, IEntityRepository<TDalEntity>
    where TDomainEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
    where TDbContext : DbContext
{
    public BaseEntityRepository(TDbContext dbContext, IDalMapper<TDomainEntity, TDalEntity> mapper) : base(dbContext)
    {
    }
}

public class BaseEntityRepository<TKey, TDomainEntity, TDalEntity, TDbContext> : IEntityRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
    where TDomainEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    public BaseEntityRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public TDalEntity Add(TDalEntity entity)
    {
        var entry = _dbContext.Add(entity);
        _dbContext.SaveChanges();
        return entry.Entity;
    }

    public async Task<TDalEntity> AddAsync(TDalEntity entity)
    {
        var entry = await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public void AddRange(IEnumerable<TDalEntity> entities)
    {
        _dbContext.AddRange(entities);
        _dbContext.SaveChanges();
    }

    public async Task AddRangeAsync(IEnumerable<TDalEntity> entities)
    {
        await _dbContext.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
    }

    public TDalEntity Update(TDalEntity entity)
    {
        var entry = _dbContext.Update(entity);
        _dbContext.SaveChanges();
        return entry.Entity;
    }

    public void UpdateRange(IEnumerable<TDalEntity> entities)
    {
        _dbContext.UpdateRange(entities);
        _dbContext.SaveChanges();
    }

    public TDalEntity Remove(TDalEntity entity)
    {
        var entry = _dbContext.Remove(entity);
        _dbContext.SaveChanges();
        return entry.Entity;
    }

    public async Task<TDalEntity> RemoveAsync(TDalEntity entity)
    {
        var entry = _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public void RemoveRange(IEnumerable<TDalEntity> entities)
    {
        _dbContext.RemoveRange(entities);
        _dbContext.SaveChanges();
    }

    public async Task RemoveRangeAsync(IEnumerable<TDalEntity> entities)
    {
        _dbContext.RemoveRange(entities);
        await _dbContext.SaveChangesAsync();
    }

    public TDalEntity? Find(TKey id)
    {
        return _dbContext.Find<TDalEntity>(id);
    }

    public async Task<TDalEntity?> FindAsync(TKey id)
    {
        return await _dbContext.FindAsync<TDalEntity>(id);
    }

    public IEnumerable<TDalEntity> FindAll()
    {
        return _dbContext.Set<TDalEntity>();
    }

    public async Task<IEnumerable<TDalEntity>> FindAllAsync()
    {
        return await _dbContext.Set<TDalEntity>().ToListAsync();
    }

    public bool Exists(TKey id)
    {
        return _dbContext.Find<TDalEntity>(id) != null;
    }

    public async Task<bool> ExistsAsync(TKey id)
    {
        return await _dbContext.FindAsync<TDalEntity>(id) != null;
    }
}