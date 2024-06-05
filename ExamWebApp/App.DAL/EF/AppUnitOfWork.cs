using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using App.DAL.EF.Repositories;
using App.Domain.Identity;
using AutoMapper;
using Base.DAL.Contracts;
using Base.DAL.EF;
using Helpers;

namespace App.DAL.EF;

public class AppUnitOfWork(AppDbContext dbContext, IMapper mapper) 
    : BaseUnitOfWork<AppDbContext>(dbContext), 
        IAppUnitOfWork
{
    private IEntityRepository<AppUser>? _users;
    public IEntityRepository<AppUser> Users => _users ??=
                                               new BaseEntityRepository<AppUser, AppUser, AppDbContext>(UowDbContext,
                                                   new EntityMapper<AppUser, AppUser>(mapper));
    
    private ISampleRepository? _samples;
    public ISampleRepository Samples => _samples ??= new SampleRepository(UowDbContext, mapper);
}