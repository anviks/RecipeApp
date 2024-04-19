using App.Domain.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IAppUserRepository : IEntityRepository<AppUser>
{
    
}