using RecipeApp.Base.Contracts.Domain;

namespace RecipeApp.Base.Contracts.Infrastructure.Data;

public interface IEntityRepository<TEntity>
    where TEntity : class, IDomainEntityId
{
    Task<TEntity?> GetByIdAsync(Guid id, bool tracking = false);
    Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<bool> ExistsAsync(Guid id);
}