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

    public virtual async Task<TBllEntity> AddAsync(TBllEntity entity)
    {
        TDalEntity? dalEntity = Mapper.Map(entity);
        TDalEntity addedEntity = await Repository.AddAsync(dalEntity!);
        return Mapper.Map(addedEntity)!;
    }

    public virtual async Task<TBllEntity> UpdateAsync(TBllEntity entity)
    {
        TDalEntity? dalEntity = Mapper.Map(entity);
        TDalEntity updatedEntity = await Repository.UpdateAsync(dalEntity!);
        return Mapper.Map(updatedEntity)!;
    }

    public virtual async Task DeleteAsync(TBllEntity entity)
    {
        TDalEntity? dalEntity = Mapper.Map(entity);
        await Repository.DeleteAsync(dalEntity!);
    }

    public virtual async Task<TBllEntity?> GetByIdAsync(TKey id, bool tracking = false)
    {
        TDalEntity? dalEntity = await Repository.GetByIdAsync(id, tracking);
        return Mapper.Map(dalEntity!);
    }

    public virtual async Task<IEnumerable<TBllEntity>> GetAllAsync(bool tracking = false)
    {
        var dalEntities = await Repository.GetAllAsync(tracking);
        return dalEntities.Select(Mapper.Map).Select(e => e!);
    }

    public virtual async Task<bool> ExistsAsync(TKey id)
    {
        return await Repository.ExistsAsync(id);
    }
}