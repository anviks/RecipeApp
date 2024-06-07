using App.DAL.Contracts.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Helpers;

namespace App.DAL.EF.Repositories;

public class CompanyRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.Company, DAL.DTO.Company, AppDbContext>(dbContext,
        new EntityMapper<Domain.Company, DAL.DTO.Company>(mapper)), ICompanyRepository
{
    
}