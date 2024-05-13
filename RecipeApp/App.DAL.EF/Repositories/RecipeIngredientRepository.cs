using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Helpers;
using DAL_DTO = App.DAL.DTO;

namespace App.DAL.EF.Repositories;

public class RecipeIngredientRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.RecipeIngredient, DAL_DTO.RecipeIngredient, AppDbContext>(dbContext,
        new EntityMapper<Domain.RecipeIngredient, DAL_DTO.RecipeIngredient>(mapper)), IRecipeIngredientRepository;