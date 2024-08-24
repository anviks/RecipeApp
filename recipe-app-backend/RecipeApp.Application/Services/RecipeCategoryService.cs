using AutoMapper;
using RecipeApp.Application.Contracts.Services;
using RecipeApp.Application.DTO;
using RecipeApp.Base.Application;
using RecipeApp.Base.Helpers;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;
using DAL = RecipeApp.Infrastructure.Data.DTO;

namespace RecipeApp.Application.Services;

public class RecipeCategoryService(
    IRecipeCategoryRepository repository,
    IMapper mapper)
    : BaseEntityService<DAL.RecipeCategory, RecipeCategory, IRecipeCategoryRepository>(repository,
            new EntityMapper<DAL.RecipeCategory, RecipeCategory>(mapper)),
        IRecipeCategoryService;