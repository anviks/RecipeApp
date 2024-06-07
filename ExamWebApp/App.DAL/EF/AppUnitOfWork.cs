using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using App.DAL.EF.Repositories;
using App.Domain;
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
    
    private IActivityRepository? _activities;
    public IActivityRepository Activities => _activities ??= new ActivityRepository(UowDbContext, mapper);
    
    private IActivityTypeRepository? _activityTypes;
    public IActivityTypeRepository ActivityTypes => _activityTypes ??= new ActivityTypeRepository(UowDbContext, mapper);
    
    private ICompanyRepository? _companies;
    public ICompanyRepository Companies => _companies ??= new CompanyRepository(UowDbContext, mapper);
    
    private IPrizeRepository? _prizes;
    public IPrizeRepository Prizes => _prizes ??= new PrizeRepository(UowDbContext, mapper);
    
    private IRaffleRepository? _raffles;
    public IRaffleRepository Raffles => _raffles ??= new RaffleRepository(UowDbContext, mapper);
    
    private IRaffleResultRepository? _raffleResults;
    public IRaffleResultRepository RaffleResults => _raffleResults ??= new RaffleResultRepository(UowDbContext, mapper);
    
    private ITicketRepository? _tickets;
    public ITicketRepository Tickets => _tickets ??= new TicketRepository(UowDbContext, mapper);
}