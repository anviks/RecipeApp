using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Base.DAL.EF;

public class BaseEntityRepository<TDomainEntity, TDalEntity, TDbContext> :
    BaseEntityRepository<Guid, TDomainEntity, TDalEntity, TDbContext>, IEntityRepository<TDalEntity>
    where TDomainEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
    where TDbContext : DbContext
{
    public BaseEntityRepository(TDbContext dbContext, IDalMapper<TDomainEntity, TDalEntity> mapper) : base(dbContext, mapper)
    {
    }
}

public class BaseEntityRepository<TKey, TDomainEntity, TDalEntity, TDbContext> : IEntityRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
    where TDomainEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TDbContext : DbContext
{
    protected readonly TDbContext DbContext;
    protected readonly DbSet<TDalEntity> DbSet;
    protected readonly IDalMapper<TDomainEntity, TDalEntity> Mapper;

    public BaseEntityRepository(TDbContext dbContext, IDalMapper<TDomainEntity, TDalEntity> mapper)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TDalEntity>();
        Mapper = mapper;
    }

    protected virtual IQueryable<TDalEntity> GetQuery(bool tracking = false)
    {
        var queryable = DbSet.AsQueryable();
        return tracking ? queryable : queryable.AsNoTracking();
    }

    public TDalEntity Add(TDalEntity entity)
    {
        var entry = DbContext.Add(entity);
        return entry.Entity;
    }

    public void AddRange(IEnumerable<TDalEntity> entities)
    {
        DbContext.AddRange(entities);
    }

    public TDalEntity Update(TDalEntity entity)
    {
        var entry = DbContext.Update(entity);
        return entry.Entity;
    }

    public void UpdateRange(IEnumerable<TDalEntity> entities)
    {
        DbContext.UpdateRange(entities);
    }

    public int Remove(TDalEntity entity)
    {
        return Remove(entity.Id);
    }

    public int Remove(TKey id)
    {
        return GetQuery().Where(e => e.Id.Equals(id)).ExecuteDelete();
    }

    public async Task<int> RemoveAsync(TDalEntity entity)
    {
        return await RemoveAsync(entity.Id);
    }

    public async Task<int> RemoveAsync(TKey id)
    {
        return await GetQuery().Where(e => e.Id.Equals(id)).ExecuteDeleteAsync();
    }

    public int RemoveRange(IEnumerable<TDalEntity> entities)
    {
        return RemoveRange(entities.Select(e => e.Id));
    }

    public int RemoveRange(IEnumerable<TKey> ids)
    {
        return GetQuery().Where(e => ids.Contains(e.Id)).ExecuteDelete();
    }

    public async Task<int> RemoveRangeAsync(IEnumerable<TDalEntity> entities)
    {
        return await RemoveRangeAsync(entities.Select(e => e.Id));
    }

    public async Task<int> RemoveRangeAsync(IEnumerable<TKey> ids)
    {
        return await GetQuery().Where(e => ids.Contains(e.Id)).ExecuteDeleteAsync();
    }

    public TDalEntity? Find(TKey id, bool tracking = false)
    {
        return GetQuery(tracking).FirstOrDefault(e => e.Id.Equals(id));
    }

    public async Task<TDalEntity?> FindAsync(TKey id, bool tracking = false)
    {
        return await GetQuery(tracking).FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public IEnumerable<TDalEntity> FindAll(IEnumerable<TKey> ids, bool tracking = false)
    {
        return GetQuery(tracking).Where(e => ids.Contains(e.Id)).ToList();
    }

    public IEnumerable<TDalEntity> FindAll(bool tracking = false)
    {
        return GetQuery(tracking).ToList();
    }

    public async Task<IEnumerable<TDalEntity>> FindAllAsync(IEnumerable<TKey> ids, bool tracking = false)
    {
        return await GetQuery(tracking).Where(e => ids.Contains(e.Id)).ToListAsync();
    }

    public async Task<IEnumerable<TDalEntity>> FindAllAsync(bool tracking = false)
    {
        return await GetQuery(tracking).ToListAsync();
    }

    public bool Exists(TKey id, bool tracking = false)
    {
        return Find(id, tracking) != null;
    }

    public async Task<bool> ExistsAsync(TKey id, bool tracking = false)
    {
        return await FindAsync(id, tracking) != null;
    }
}