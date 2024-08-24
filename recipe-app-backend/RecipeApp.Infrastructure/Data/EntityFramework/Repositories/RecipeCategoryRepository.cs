using AutoMapper;
using RecipeApp.Base.Helpers;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Repositories;

public class RecipeCategoryRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Entities.RecipeCategory, DTO.RecipeCategory, AppDbContext>(dbContext,
        new EntityMapper<Entities.RecipeCategory, DTO.RecipeCategory>(mapper)), IRecipeCategoryRepository;