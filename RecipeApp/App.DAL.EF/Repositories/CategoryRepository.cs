using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Helpers;
using DAL_DTO = App.DAL.DTO;

namespace App.DAL.EF.Repositories;

public class CategoryRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.Category, DAL_DTO.Category, AppDbContext>(
            dbContext,
            new EntityMapper<Domain.Category, DAL_DTO.Category>(mapper)),
        ICategoryRepository;