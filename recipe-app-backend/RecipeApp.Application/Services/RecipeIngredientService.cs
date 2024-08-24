using AutoMapper;
using RecipeApp.Application.Contracts.Services;
using RecipeApp.Application.DTO;
using RecipeApp.Base.Application;
using RecipeApp.Base.Helpers;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;
using DAL = RecipeApp.Infrastructure.Data.DTO;

namespace RecipeApp.Application.Services;

public class RecipeIngredientService(
    IRecipeIngredientRepository repository,
    IMapper mapper)
    : BaseEntityService<DAL.RecipeIngredient, RecipeIngredient, IRecipeIngredientRepository>(repository,
            new EntityMapper<DAL.RecipeIngredient, RecipeIngredient>(mapper)),
        IRecipeIngredientService;