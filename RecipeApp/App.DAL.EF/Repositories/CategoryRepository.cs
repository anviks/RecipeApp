using App.Contracts.DAL.Repositories;
using App.Domain;
using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CategoryRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Category, Category, AppDbContext>(
            dbContext,
            new DalDomainMapper<Category, Category>(mapper)
        ),
        ICategoryRepository;