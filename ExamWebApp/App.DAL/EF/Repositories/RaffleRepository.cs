using App.DAL.Contracts.Repositories;
using App.DAL.DTO;
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
    
    public async Task<IEnumerable<Raffle>> FindAllWithAccessAsync(bool isAdmin, Guid? companyId)
    {
        var query = dbContext.Raffles.AsQueryable();

        if (!isAdmin)
        {
            query = query.Where(r => r.VisibleToPublic || (companyId != null && r.CompanyId == companyId));
        }

        return (await query.ToListAsync()).Select(mapper.Map<Raffle>);
    }
}