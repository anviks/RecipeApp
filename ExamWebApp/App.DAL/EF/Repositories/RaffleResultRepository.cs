using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RaffleResultRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.RaffleResult, DAL.DTO.RaffleResult, AppDbContext>(dbContext,
        new EntityMapper<Domain.RaffleResult, DAL.DTO.RaffleResult>(mapper)), IRaffleResultRepository
{
    protected override IQueryable<Domain.RaffleResult> GetQuery(bool tracking = false)
    {
        var queryable = base.GetQuery(tracking);
        return queryable.Include(a => a.User).Include(a => a.Raffle);
    }
}