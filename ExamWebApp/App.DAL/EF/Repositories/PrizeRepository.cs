using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PrizeRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.Prize, DAL.DTO.Prize, AppDbContext>(dbContext,
        new EntityMapper<Domain.Prize, DAL.DTO.Prize>(mapper)), IPrizeRepository
{
    protected override IQueryable<Domain.Prize> GetQuery(bool tracking = false)
    {
        var queryable = base.GetQuery(tracking);
        return queryable.Include(p => p.RaffleResult).Include(p => p.Raffle);
    }
}