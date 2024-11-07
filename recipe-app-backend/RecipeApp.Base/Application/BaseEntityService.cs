using RecipeApp.Base.Contracts.Application;
using RecipeApp.Base.Contracts.Domain;
using RecipeApp.Base.Contracts.Infrastructure.Data;
using RecipeApp.Base.Helpers;

namespace RecipeApp.Base.Application;

public class BaseEntityService<TDalEntity, TBllEntity, TRepository>(
    TRepository repository,
    EntityMapper<TDalEntity, TBllEntity> mapper)
    : IEntityService<TBllEntity>
    where TRepository : IEntityRepository<TDalEntity>
    where TBllEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
{
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

    public virtual async Task<TBllEntity?> GetByIdAsync(Guid id, bool tracking = false)
    {
        TDalEntity? dalEntity = await Repository.GetByIdAsync(id, tracking);
        return Mapper.Map(dalEntity!);
    }

    public virtual async Task<IEnumerable<TBllEntity>> GetAllAsync(bool tracking = false)
    {
        var dalEntities = await Repository.GetAllAsync(tracking);
        return dalEntities.Select(Mapper.Map).Select(e => e!);
    }

    public virtual async Task<bool> ExistsAsync(Guid id)
    {
        return await Repository.ExistsAsync(id);
    }
}