using Microsoft.AspNetCore.Identity;

namespace Base.Domain.Contracts;

public interface IDomainAppUser<TUser> : IDomainAppUser<Guid, TUser>, IDomainAppUserId
    where TUser : IdentityUser<Guid>
{
}

public interface IDomainAppUser<TKey, TUser> : IDomainAppUserId<TKey>
    where TKey : IEquatable<TKey>
    where TUser : IdentityUser<TKey>
{
    public TUser? AppUser { get; set; }
}