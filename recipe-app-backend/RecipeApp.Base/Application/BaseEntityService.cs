using Base.Contracts.Domain;
using RecipeApp.Base.Contracts.Application;
using RecipeApp.Base.Contracts.Infrastructure.Data;
using RecipeApp.Base.Helpers;

namespace RecipeApp.Base.Application;

public class BaseEntityService<TDalEntity, TBllEntity, TRepository>(
    // IUnitOfWork unitOfWork,
    TRepository repository,
    EntityMapper<TDalEntity, TBllEntity> mapper)
    : BaseEntityService<TDalEntity, TBllEntity, TRepository, Guid>(/*unitOfWork, */repository, mapper),
        IEntityService<TBllEntity>
    where TBllEntity : class, IDomainEntityId
    where TRepository : IEntityRepository<TDalEntity, Guid>
    where TDalEntity : class, IDomainEntityId<Guid>;

public class BaseEntityService<TDalEntity, TBllEntity, TRepository, TKey>(
    // IUnitOfWork unitOfWork,
    TRepository repository,
    EntityMapper<TDalEntity, TBllEntity> mapper)
    : IEntityService<TBllEntity, TKey>
    where TRepository : IEntityRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
{
    // private readonly IUnitOfWork _unitOfWork = unitOfWork;
    protected readonly TRepository Repository = repository;
    protected readonly EntityMapper<TDalEntity, TBllEntity> Mapper = mapper;

    public virtual TBllEntity Add(TBllEntity entity)
    {
        TDalEntity? dalEntity = Mapper.Map(entity);
        TDalEntity addedEntity = Repository.Add(dalEntity!);
        return Mapper.Map(addedEntity)!;
    }

    public virtual void AddRange(IEnumerable<TBllEntity> entities)
    {
        var dalEntities = entities.Select(Mapper.Map).Select(e => e!);
        Repository.AddRange(dalEntities);
    }

    public virtual TBllEntity Update(TBllEntity entity)
    {
        TDalEntity? dalEntity = Mapper.Map(entity);
        TDalEntity updatedEntity = Repository.Update(dalEntity!);
        return Mapper.Map(updatedEntity)!;
    }

    public virtual void UpdateRange(IEnumerable<TBllEntity> entities)
    {
        var dalEntities = entities.Select(Mapper.Map).Select(e => e!);
        Repository.UpdateRange(dalEntities);
    }

    public virtual int Remove(TBllEntity entity)
    {
        TDalEntity? dalEntity = Mapper.Map(entity);
        return Repository.Remove(dalEntity!);
    }

    public virtual int Remove(TKey id)
    {
        return Repository.Remove(id);
    }

    public virtual async Task<int> RemoveAsync(TBllEntity entity)
    {
        TDalEntity? dalEntity = Mapper.Map(entity);
        return await Repository.RemoveAsync(dalEntity!);
    }

    public virtual async Task<int> RemoveAsync(TKey id)
    {
        return await Repository.RemoveAsync(id);
    }

    public virtual int RemoveRange(IEnumerable<TBllEntity> entities)
    {
        var dalEntities = entities.Select(Mapper.Map).Select(e => e!);
        return Repository.RemoveRange(dalEntities);
    }

    public virtual int RemoveRange(IEnumerable<TKey> ids)
    {
        return Repository.RemoveRange(ids);
    }

    public virtual TBllEntity? Find(TKey id, bool tracking = false)
    {
        TDalEntity? dalEntity = Repository.Find(id, tracking);
        return Mapper.Map(dalEntity!);
    }

    public virtual async Task<TBllEntity?> FindAsync(TKey id, bool tracking = false)
    {
        TDalEntity? dalEntity = await Repository.FindAsync(id, tracking);
        return Mapper.Map(dalEntity!);
    }

    public virtual IEnumerable<TBllEntity> FindAll(bool tracking = false)
    {
        var dalEntities = Repository.FindAll(tracking);
        return dalEntities.Select(Mapper.Map).Select(e => e!);
    }

    public virtual async Task<IEnumerable<TBllEntity>> FindAllAsync(bool tracking = false)
    {
        var dalEntities = await Repository.FindAllAsync(tracking);
        return dalEntities.Select(Mapper.Map).Select(e => e!);
    }

    public virtual bool Exists(TKey id, bool tracking = false)
    {
        return Repository.Exists(id, tracking);
    }

    public virtual async Task<bool> ExistsAsync(TKey id, bool tracking = false)
    {
        return await Repository.ExistsAsync(id, tracking);
    }
}