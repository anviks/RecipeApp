using Base.DAL.Contracts;
using Base.Domain.Contracts;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseEntityRepository<TDomainEntity, TDalEntity, TDbContext>(
    TDbContext dbContext,
    EntityMapper<TDomainEntity, TDalEntity> mapper)
    : BaseEntityRepository<Guid, TDomainEntity, TDalEntity, TDbContext>(dbContext, mapper),
        IEntityRepository<TDalEntity>
    where TDomainEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
    where TDbContext : DbContext;

public class BaseEntityRepository<TKey, TDomainEntity, TDalEntity, TDbContext> : IEntityRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
    where TDomainEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TDbContext : DbContext
{
    protected readonly TDbContext DbContext;
    protected readonly DbSet<TDomainEntity> DbSet;
    protected readonly EntityMapper<TDomainEntity, TDalEntity> Mapper;

    protected BaseEntityRepository(TDbContext dbContext, EntityMapper<TDomainEntity, TDalEntity> mapper)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TDomainEntity>();
        Mapper = mapper;
    }

    protected virtual IQueryable<TDomainEntity> GetQuery(bool tracking = false)
    {
        var queryable = DbSet.AsQueryable();
        return tracking ? queryable : queryable.AsNoTracking();
    }

    public virtual TDalEntity Add(TDalEntity entity)
    {
        var entry = DbContext.Add(Mapper.Map(entity)!);
        return Mapper.Map(entry.Entity)!;
    }

    public virtual void AddRange(IEnumerable<TDalEntity> entities)
    {
        DbContext.AddRange(entities.Select(Mapper.Map)!);
    }

    public virtual TDalEntity Update(TDalEntity entity)
    {
        var entry = DbContext.Update(Mapper.Map(entity)!);
        return Mapper.Map(entry.Entity)!;
    }

    public virtual void UpdateRange(IEnumerable<TDalEntity> entities)
    {
        DbContext.UpdateRange(entities.Select(Mapper.Map)!);
    }

    public virtual int Remove(TDalEntity entity)
    {
        return Remove(entity.Id);
    }

    public virtual int Remove(TKey id)
    {
        TDomainEntity? entity = DbSet.Find(id);
        if (entity == null) return 0;
        DbContext.Remove(entity);
        return 1;
    }

    public virtual async Task<int> RemoveAsync(TDalEntity entity)
    {
        return await RemoveAsync(entity.Id);
    }

    public virtual async Task<int> RemoveAsync(TKey id)
    {
        TDomainEntity? entity = await DbSet.FindAsync(id);
        if (entity == null) return 0;
        DbSet.Remove(entity);
        return 1;
    }

    public virtual int RemoveRange(IEnumerable<TDalEntity> entities)
    {
        return RemoveRange(entities.Select(e => e.Id));
    }

    public virtual int RemoveRange(IEnumerable<TKey> ids)
    {
        var entities = DbSet.Where(e => ids.Contains(e.Id));
        DbSet.RemoveRange(entities);
        return entities.Count();
    }

    public virtual TDalEntity? Find(TKey id, bool tracking = false)
    {
        return Mapper.Map(GetQuery(tracking).FirstOrDefault(e => e.Id.Equals(id)));
    }

    public virtual async Task<TDalEntity?> FindAsync(TKey id, bool tracking = false)
    {
        return Mapper.Map(await GetQuery(tracking).FirstOrDefaultAsync(e => e.Id.Equals(id)));
    }

    public virtual IEnumerable<TDalEntity> FindAll(bool tracking = false)
    {
        return GetQuery(tracking).AsEnumerable().Select(Mapper.Map)!;
    }

    public virtual async Task<IEnumerable<TDalEntity>> FindAllAsync(bool tracking = false)
    {
        return (await GetQuery(tracking).ToListAsync()).Select(Mapper.Map)!;
    }

    public virtual bool Exists(TKey id, bool tracking = false)
    {
        return DbSet.Any(e => e.Id.Equals(id));
    }

    public virtual async Task<bool> ExistsAsync(TKey id, bool tracking = false)
    {
        return await DbSet.AnyAsync(e => e.Id.Equals(id));
    }
}