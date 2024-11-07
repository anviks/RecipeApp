using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Base.Contracts.Domain;
using RecipeApp.Base.Contracts.Infrastructure.Data;
using RecipeApp.Base.Helpers;

namespace RecipeApp.Base.Infrastructure.Data;

public class BaseEntityRepository<TDomainEntity, TDalEntity, TDbContext> : IEntityRepository<TDalEntity>
    where TDomainEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
    where TDbContext : DbContext
{
    protected readonly TDbContext DbContext;
    protected readonly DbSet<TDomainEntity> DbSet;
    protected readonly EntityMapper<TDomainEntity, TDalEntity> Mapper;

    public BaseEntityRepository(TDbContext dbContext, IMapper mapper)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TDomainEntity>();
        Mapper = new EntityMapper<TDomainEntity, TDalEntity>(mapper);
    }

    protected virtual IQueryable<TDomainEntity> GetQuery(bool tracking = false)
    {
        return tracking ? DbSet.AsTracking() : DbSet.AsNoTracking();
    }

    public virtual async Task<TDalEntity?> GetByIdAsync(Guid id, bool tracking = false)
    {
        return Mapper.Map(await GetQuery(tracking).FirstOrDefaultAsync(e => e.Id.Equals(id)));
    }

    public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(bool tracking = false)
    {
        return (await GetQuery(tracking).ToListAsync()).Select(Mapper.Map)!;
    }

    public virtual async Task<TDalEntity> AddAsync(TDalEntity entity)
    {
        var entry = await DbContext.AddAsync(Mapper.Map(entity)!);
        return Mapper.Map(entry.Entity)!;
    }

    public virtual Task<TDalEntity> UpdateAsync(TDalEntity entity)
    {
        var entry = DbContext.Update(Mapper.Map(entity)!);
        return Task.FromResult(Mapper.Map(entry.Entity)!);
    }

    public virtual Task DeleteAsync(TDalEntity entity)
    {
        DbContext.Remove(Mapper.Map(entity)!);
        return Task.CompletedTask;
    }

    public virtual async Task<bool> ExistsAsync(Guid id)
    {
        return await DbSet.AsNoTracking().AnyAsync(e => e.Id.Equals(id));
    }
}