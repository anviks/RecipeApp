using RecipeApp.Base.Contracts.Infrastructure.Data;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;
using RecipeApp.Infrastructure.Data.EntityFramework.Entities.Identity;

namespace RecipeApp.Infrastructure.Contracts.Data;

public interface IAppUnitOfWork : IUnitOfWork
{
    IEntityRepository<AppUser> Users { get; }
    ICategoryRepository Categories { get; }
    IIngredientRepository Ingredients { get; }
    IIngredientTypeAssociationRepository IngredientTypeAssociations { get; }
    IIngredientTypeRepository IngredientTypes { get; }
    IRecipeCategoryRepository RecipeCategories { get; }
    IRecipeIngredientRepository RecipeIngredients { get; }
    IRecipeRepository Recipes { get; }
    IReviewRepository Reviews { get; }
    IUnitRepository Units { get; }
}