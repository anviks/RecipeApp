using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Helpers;
using Microsoft.EntityFrameworkCore;

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
}