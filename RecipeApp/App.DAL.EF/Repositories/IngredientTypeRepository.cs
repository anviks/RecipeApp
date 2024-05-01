using App.Contracts.DAL.Repositories;
using App.Domain;
using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class IngredientTypeRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<IngredientType, IngredientType, AppDbContext>(dbContext,
        new DalDomainMapper<IngredientType, IngredientType>(mapper)), IIngredientTypeRepository;