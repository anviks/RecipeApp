using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF.Repositories;
using App.Domain.Identity;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    public AppUnitOfWork(AppDbContext dbContext) : base(dbContext)
    {
    }
    
    private IEntityRepository<AppUser>? _users;

    public IEntityRepository<AppUser> Users => _users ??=
                                               new BaseEntityRepository<AppUser, AppUser, AppDbContext>(UowDbContext,
                                                   new DalDummyMapper<AppUser, AppUser>());
    
    private ICategoryRepository? _categories;
    public ICategoryRepository Categories => _categories ??= new CategoryRepository(UowDbContext);
    
    private IIngredientRepository? _ingredients;
    public IIngredientRepository Ingredients => _ingredients ??= new IngredientRepository(UowDbContext);
    
    private IIngredientTypeAssociationRepository? _ingredientTypeAssociations;
    public IIngredientTypeAssociationRepository IngredientTypeAssociations => _ingredientTypeAssociations ??= new IngredientTypeAssociationRepository(UowDbContext);
    
    private IIngredientTypeRepository? _ingredientTypes;
    public IIngredientTypeRepository IngredientTypes => _ingredientTypes ??= new IngredientTypeRepository(UowDbContext);
    
    private IRecipeCategoryRepository? _recipeCategories;
    public IRecipeCategoryRepository RecipeCategories => _recipeCategories ??= new RecipeCategoryRepository(UowDbContext);
    
    private IRecipeIngredientRepository? _recipeIngredients;
    public IRecipeIngredientRepository RecipeIngredients => _recipeIngredients ??= new RecipeIngredientRepository(UowDbContext);
    
    private IRecipeRepository? _recipes;
    public IRecipeRepository Recipes => _recipes ??= new RecipeRepository(UowDbContext);
    
    private IReviewRepository? _reviews;
    public IReviewRepository Reviews => _reviews ??= new ReviewRepository(UowDbContext);
    
    private IUnitRepository? _units;
    public IUnitRepository Units => _units ??= new UnitRepository(UowDbContext);
}