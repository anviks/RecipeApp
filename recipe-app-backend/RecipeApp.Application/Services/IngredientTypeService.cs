using AutoMapper;
using RecipeApp.Application.Contracts.Services;
using RecipeApp.Application.DTO;
using RecipeApp.Base.Application;
using RecipeApp.Base.Helpers;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;
using DAL = RecipeApp.Infrastructure.Data.DTO;

namespace RecipeApp.Application.Services;

public class IngredientTypeService(
    IIngredientTypeRepository repository,
    IMapper mapper)
    : BaseEntityService<DAL.IngredientType, IngredientType, IIngredientTypeRepository>(repository,
            new EntityMapper<DAL.IngredientType, IngredientType>(mapper)),
        IIngredientTypeService;