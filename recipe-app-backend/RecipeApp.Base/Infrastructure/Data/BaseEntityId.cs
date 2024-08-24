using Base.Contracts.Domain;

namespace RecipeApp.Base.Infrastructure.Data;

public abstract class BaseEntityId : BaseEntityId<Guid>, IDomainEntityId
{
}

public abstract class BaseEntityId<TKey>: IDomainEntityId<TKey> 
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
}