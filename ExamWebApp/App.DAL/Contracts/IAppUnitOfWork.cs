using App.DAL.Contracts.Repositories;
using App.Domain.Identity;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IAppUnitOfWork
{
    IEntityRepository<AppUser> Users { get; }
    ISampleRepository Samples { get; }
}