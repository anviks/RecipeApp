using App.Contracts.DAL.Repositories;
using App.Domain;
using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class IngredientRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Ingredient, Ingredient, AppDbContext>(dbContext,
        new DalDomainMapper<Ingredient, Ingredient>(mapper)), IIngredientRepository;