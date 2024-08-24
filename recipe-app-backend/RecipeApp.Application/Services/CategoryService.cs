using AutoMapper;
using RecipeApp.Application.Contracts.Services;
using RecipeApp.Application.DTO;
using RecipeApp.Base.Application;
using RecipeApp.Base.Helpers;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;
using DAL = RecipeApp.Infrastructure.Data.DTO;

namespace RecipeApp.Application.Services;

public class CategoryService(
    ICategoryRepository repository,
    IMapper mapper)
    : BaseEntityService<DAL.Category, Category, ICategoryRepository>(repository,
            new EntityMapper<DAL.Category, Category>(mapper)),
        ICategoryService;