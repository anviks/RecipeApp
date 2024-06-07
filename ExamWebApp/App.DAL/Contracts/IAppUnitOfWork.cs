using App.DAL.Contracts.Repositories;
using App.Domain.Identity;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IAppUnitOfWork : IUnitOfWork
{
    IEntityRepository<AppUser> Users { get; }
    ISampleRepository Samples { get; }
    IActivityRepository Activities { get; }
    IActivityTypeRepository ActivityTypes { get; }
    ICompanyRepository Companies { get; }
    IPrizeRepository Prizes { get; }
    IRaffleRepository Raffles { get; }
    IRaffleResultRepository RaffleResults { get; }
    ITicketRepository Tickets { get; }
}