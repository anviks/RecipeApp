using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RaffleRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.Raffle, DAL.DTO.Raffle, AppDbContext>(dbContext,
        new EntityMapper<Domain.Raffle, DAL.DTO.Raffle>(mapper)), IRaffleRepository
{
    protected override IQueryable<Domain.Raffle> GetQuery(bool tracking = false)
    {
        var queryable = base.GetQuery(tracking);
        return queryable.Include(a => a.Company);
    }
}