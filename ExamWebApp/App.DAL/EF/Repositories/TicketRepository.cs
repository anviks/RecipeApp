using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TicketRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.Ticket, DAL.DTO.Ticket, AppDbContext>(dbContext,
        new EntityMapper<Domain.Ticket, DAL.DTO.Ticket>(mapper)), ITicketRepository
{
    protected override IQueryable<Domain.Ticket> GetQuery(bool tracking = false)
    {
        var queryable = base.GetQuery(tracking);
        return queryable.Include(a => a.User).Include(a => a.Raffle);
    }
}