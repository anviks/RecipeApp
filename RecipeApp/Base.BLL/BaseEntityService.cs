using Base.Contracts.BLL;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace Base.BLL;

public class BaseEntityService<TDalEntity, TBllEntity, TRepository>(
    IUnitOfWork unitOfWork,
    TRepository repository,
    IBllMapper<TDalEntity, TBllEntity> mapper)
    : BaseEntityService<TDalEntity, TBllEntity, TRepository, Guid>(unitOfWork, repository, mapper),
        IEntityService<TBllEntity>
    where TBllEntity : class, IDomainEntityId
    where TRepository : IEntityRepository<TDalEntity, Guid>
    where TDalEntity : class, IDomainEntityId<Guid>;

public class BaseEntityService<TDalEntity, TBllEntity, TRepository, TKey>(
    IUnitOfWork unitOfWork,
    TRepository repository,
    IBllMapper<TDalEntity, TBllEntity> mapper)
    : IEntityService<TBllEntity, TKey>
    where TRepository : IEntityRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    protected readonly TRepository Repository = repository;
    protected readonly IBllMapper<TDalEntity, TBllEntity> Mapper = mapper;

    public TBllEntity Add(TBllEntity entity)
    {
        TDalEntity? dalEntity = Mapper.Map(entity);
        TDalEntity addedEntity = Repository.Add(dalEntity!);
        return Mapper.Map(addedEntity)!;
    }

    public void AddRange(IEnumerable<TBllEntity> entities)
    {
        var dalEntities = entities.Select(Mapper.Map).Select(e => e!);
        Repository.AddRange(dalEntities);
    }

    public TBllEntity Update(TBllEntity entity)
    {
        TDalEntity? dalEntity = Mapper.Map(entity);
        TDalEntity updatedEntity = Repository.Update(dalEntity!);
        return Mapper.Map(updatedEntity)!;
    }

    public void UpdateRange(IEnumerable<TBllEntity> entities)
    {
        var dalEntities = entities.Select(Mapper.Map).Select(e => e!);
        Repository.UpdateRange(dalEntities);
    }

    public int Remove(TBllEntity entity)
    {
        TDalEntity? dalEntity = Mapper.Map(entity);
        return Repository.Remove(dalEntity!);
    }

    public int Remove(TKey id)
    {
        return Repository.Remove(id);
    }

    public async Task<int> RemoveAsync(TBllEntity entity)
    {
        TDalEntity? dalEntity = Mapper.Map(entity);
        return await Repository.RemoveAsync(dalEntity!);
    }

    public async Task<int> RemoveAsync(TKey id)
    {
        return await Repository.RemoveAsync(id);
    }

    public int RemoveRange(IEnumerable<TBllEntity> entities)
    {
        var dalEntities = entities.Select(Mapper.Map).Select(e => e!);
        return Repository.RemoveRange(dalEntities);
    }

    public int RemoveRange(IEnumerable<TKey> ids)
    {
        return Repository.RemoveRange(ids);
    }

    public async Task<int> RemoveRangeAsync(IEnumerable<TBllEntity> entities)
    {
        var dalEntities = entities.Select(Mapper.Map).Select(e => e!);
        return await Repository.RemoveRangeAsync(dalEntities);
    }

    public async Task<int> RemoveRangeAsync(IEnumerable<TKey> ids)
    {
        return await Repository.RemoveRangeAsync(ids);
    }

    public TBllEntity? Find(TKey id, bool tracking = false)
    {
        TDalEntity? dalEntity = Repository.Find(id, tracking);
        return Mapper.Map(dalEntity!);
    }

    public async Task<TBllEntity?> FindAsync(TKey id, bool tracking = false)
    {
        TDalEntity? dalEntity = await Repository.FindAsync(id, tracking);
        return Mapper.Map(dalEntity!);
    }

    public IEnumerable<TBllEntity> FindAll(IEnumerable<TKey> ids, bool tracking = false)
    {
        var dalEntities = Repository.FindAll(ids, tracking);
        return dalEntities.Select(Mapper.Map).Select(e => e!);
    }

    public IEnumerable<TBllEntity> FindAll(bool tracking = false)
    {
        var dalEntities = Repository.FindAll(tracking);
        return dalEntities.Select(Mapper.Map).Select(e => e!);
    }

    public async Task<IEnumerable<TBllEntity>> FindAllAsync(IEnumerable<TKey> ids, bool tracking = false)
    {
        var dalEntities = await Repository.FindAllAsync(ids, tracking);
        return dalEntities.Select(Mapper.Map).Select(e => e!);
    }

    public async Task<IEnumerable<TBllEntity>> FindAllAsync(bool tracking = false)
    {
        var dalEntities = await Repository.FindAllAsync(tracking);
        return dalEntities.Select(Mapper.Map).Select(e => e!);
    }

    public bool Exists(TKey id, bool tracking = false)
    {
        return Repository.Exists(id, tracking);
    }

    public async Task<bool> ExistsAsync(TKey id, bool tracking = false)
    {
        return await Repository.ExistsAsync(id, tracking);
    }
}