using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace Base.Domain;

public abstract class BaseEntityIdMetadata : BaseEntityIdMetadata<Guid>
{
}

public abstract class BaseEntityIdMetadata<TKey> : BaseEntityId<TKey>, IDomainEntityMetadata
    where TKey : IEquatable<TKey>
{
    [MaxLength(128)] 
    public string CreatedBy { get; set; } = default!;
    public DateTime CreatedAt { get; set; }

    [MaxLength(128)] 
    public string UpdatedBy { get; set; } = default!;
    public DateTime UpdatedAt { get; set; }
}