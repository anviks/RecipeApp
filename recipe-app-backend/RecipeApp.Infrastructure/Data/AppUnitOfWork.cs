using App.DAL.EF;
using AutoMapper;
using RecipeApp.Base.Contracts.Infrastructure.Data;
using RecipeApp.Base.Helpers;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Contracts.Data;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;
using RecipeApp.Infrastructure.Data.EntityFramework;
using RecipeApp.Infrastructure.Data.EntityFramework.Entities.Identity;
using RecipeApp.Infrastructure.Data.EntityFramework.Repositories;

namespace RecipeApp.Infrastructure.Data;

public class AppUnitOfWork(AppDbContext dbContext, IMapper mapper) 
    : BaseUnitOfWork<AppDbContext>(dbContext), 
        IAppUnitOfWork
{
    private IEntityRepository<AppUser>? _users;
    public IEntityRepository<AppUser> Users => _users ??=
                                               new BaseEntityRepository<AppUser, AppUser, AppDbContext>(UowDbContext, mapper);
    
    private ICategoryRepository? _categories;
    public ICategoryRepository Categories => _categories ??= new CategoryRepository(UowDbContext, mapper);
    
    private IIngredientRepository? _ingredients;
    public IIngredientRepository Ingredients => _ingredients ??= new IngredientRepository(UowDbContext, mapper);
    
    private IIngredientTypeAssociationRepository? _ingredientTypeAssociations;
    public IIngredientTypeAssociationRepository IngredientTypeAssociations => _ingredientTypeAssociations ??= new IngredientTypeAssociationRepository(UowDbContext, mapper);
    
    private IIngredientTypeRepository? _ingredientTypes;
    public IIngredientTypeRepository IngredientTypes => _ingredientTypes ??= new IngredientTypeRepository(UowDbContext, mapper);
    
    private IRecipeCategoryRepository? _recipeCategories;
    public IRecipeCategoryRepository RecipeCategories => _recipeCategories ??= new RecipeCategoryRepository(UowDbContext, mapper);
    
    private IRecipeIngredientRepository? _recipeIngredients;
    public IRecipeIngredientRepository RecipeIngredients => _recipeIngredients ??= new RecipeIngredientRepository(UowDbContext, mapper);
    
    private IRecipeRepository? _recipes;
    public IRecipeRepository Recipes => _recipes ??= new RecipeRepository(UowDbContext, mapper);
    
    private IReviewRepository? _reviews;
    public IReviewRepository Reviews => _reviews ??= new ReviewRepository(UowDbContext, mapper);
    
    private IUnitRepository? _units;
    public IUnitRepository Units => _units ??= new UnitRepository(UowDbContext, mapper);
}