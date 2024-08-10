using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Domain.Identity;
using AutoMapper;
using Base.BLL;
using Base.Contracts.BLL;

namespace App.BLL;

public class AppBusinessLogic(IAppUnitOfWork unitOfWork, IMapper mapper)
    : BaseBusinessLogic(unitOfWork), IAppBusinessLogic
{
    private ICategoryService? _categories;
    public ICategoryService Categories => _categories ??= new CategoryService(unitOfWork.Categories, mapper);
    
    private IIngredientService? _ingredients;
    public IIngredientService Ingredients =>
        _ingredients ??= new IngredientService(unitOfWork.Ingredients, mapper);

    private IIngredientTypeAssociationService? _ingredientTypeAssociations;
    public IIngredientTypeAssociationService IngredientTypeAssociations => _ingredientTypeAssociations ??=
        new IngredientTypeAssociationService(unitOfWork.IngredientTypeAssociations, mapper);

    private IIngredientTypeService? _ingredientTypes;
    public IIngredientTypeService IngredientTypes => _ingredientTypes ??=
        new IngredientTypeService(unitOfWork.IngredientTypes, mapper);
    
    private IRecipeCategoryService? _recipeCategories;
    public IRecipeCategoryService RecipeCategories => _recipeCategories ??=
        new RecipeCategoryService(unitOfWork.RecipeCategories, mapper);
    
    private IRecipeIngredientService? _recipeIngredients;
    public IRecipeIngredientService RecipeIngredients => _recipeIngredients ??=
        new RecipeIngredientService(unitOfWork.RecipeIngredients, mapper);

    private IRecipeService? _recipes;
    public IRecipeService Recipes => _recipes ??= new RecipeService(unitOfWork.Recipes, mapper);
    
    private IReviewService? _reviews;
    public IReviewService Reviews => _reviews ??= new ReviewService(unitOfWork.Reviews, mapper);
    
    private IUnitService? _units;
    public IUnitService Units => _units ??= new UnitService(unitOfWork.Units, mapper);
}