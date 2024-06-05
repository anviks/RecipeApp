using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Helpers;

namespace App.DAL.EF.Repositories;

public class SampleRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.Sample, DAL.DTO.Sample, AppDbContext>(dbContext,
        new EntityMapper<Domain.Sample, DAL.DTO.Sample>(mapper)), ISampleRepository
{
    
}