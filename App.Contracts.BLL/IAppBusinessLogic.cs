using App.Contracts.BLL.Services;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

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