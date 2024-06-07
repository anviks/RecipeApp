using App.DAL.Contracts.Repositories;
using App.DAL.DTO;
using AutoMapper;
using Base.DAL.EF;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Ticket = App.Domain.Ticket;

namespace App.DAL.EF.Repositories;

public class ActivityRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.Activity, DAL.DTO.Activity, AppDbContext>(dbContext,
        new EntityMapper<Domain.Activity, DAL.DTO.Activity>(mapper)), IActivityRepository
{
    protected override IQueryable<Domain.Activity> GetQuery(bool tracking = false)
    {
        var queryable = base.GetQuery(tracking);
        return queryable.Include(a => a.User).Include(a => a.ActivityType);
    }

    public override Activity Add(Activity entity)
    {
        entity.Date = DateTime.Now.ToUniversalTime();
        return base.Add(entity);
    }
    
    public override Activity Update(Activity entity)
    {
        var dbEntity = Find(entity.Id)!;
        
        var userId = dbEntity.UserId;
        var userTotalMinutes = GetQuery()
            .Where(a => a.UserId == userId)
            .Sum(a => a.DurationInMinutes);

        DateTime currentTime = DateTime.Now.ToUniversalTime();
        TimeSpan elapsedTime = currentTime - dbEntity.Date.ToUniversalTime();
        dbEntity.DurationInMinutes = (int)elapsedTime.TotalMinutes;

        if (dbEntity.DurationInMinutes + userTotalMinutes > 1000)
        {
            var previouslyAcquiredTickets = (userTotalMinutes - 1000) / 10;
            var deservedTickets = (dbEntity.DurationInMinutes + userTotalMinutes - 1000) / 10;
            var ticketsToAcquire = deservedTickets - previouslyAcquiredTickets;
            
            var raffles = dbContext.Raffles
                .Where(r => r.StartDate <= currentTime && r.EndDate >= currentTime)
                .ToList();

            for (var i = 0; i < ticketsToAcquire; i++)
            {
                foreach (Domain.Raffle raffle in raffles)
                {
                    dbContext.Tickets.Add(new Ticket
                    {
                        RaffleId = raffle.Id,
                        UserId = userId
                    });
                }
            }
            
            dbContext.SaveChanges();
        }
        
        return base.Update(dbEntity);
    }
}