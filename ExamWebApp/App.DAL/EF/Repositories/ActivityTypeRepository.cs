using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Helpers;

namespace App.DAL.EF.Repositories;

public class ActivityTypeRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.ActivityType, DAL.DTO.ActivityType, AppDbContext>(dbContext,
        new EntityMapper<Domain.ActivityType, DAL.DTO.ActivityType>(mapper)), IActivityTypeRepository
{
    
}