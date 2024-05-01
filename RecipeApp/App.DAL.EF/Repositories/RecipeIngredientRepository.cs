using App.Contracts.DAL.Repositories;
using App.Domain;
using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class RecipeIngredientRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<RecipeIngredient, RecipeIngredient, AppDbContext>(dbContext,
        new DalDomainMapper<RecipeIngredient, RecipeIngredient>(mapper)), IRecipeIngredientRepository;