using AutoMapper;
using RecipeApp.Application.Contracts.Services;
using RecipeApp.Base.Application;
using RecipeApp.Base.Helpers;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;
using RecipeApp.Infrastructure.Data.DTO;

namespace RecipeApp.Application.Services;

public class IngredientService(
    IIngredientRepository repository,
    IMapper mapper)
    : BaseEntityService<Ingredient, DTO.Ingredient, IIngredientRepository>(repository,
            new EntityMapper<Ingredient, DTO.Ingredient>(mapper)),
        IIngredientService;