using Base.Domain.Contracts;

namespace Base.Domain;

public abstract class BaseEntityId : BaseEntityId<Guid>, IDomainEntityId
{
}

public abstract class BaseEntityId<TKey>: IDomainEntityId<TKey> 
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
}