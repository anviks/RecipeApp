using RecipeApp.Application.Contracts.Services;
using RecipeApp.Base.Contracts.Application;

namespace RecipeApp.Application.Contracts;

public interface IAppBusinessLogic : IBusinessLogic
{
    ICategoryService Categories { get; }
    IIngredientService Ingredients { get; }
    IIngredientTypeAssociationService IngredientTypeAssociations { get; }
    IIngredientTypeService IngredientTypes { get; }
    IRecipeCategoryService RecipeCategories { get; }
    IRecipeIngredientService RecipeIngredients { get; }
    IRecipeService Recipes { get; }
    IReviewService Reviews { get; }
    IUnitService Units { get; }
}