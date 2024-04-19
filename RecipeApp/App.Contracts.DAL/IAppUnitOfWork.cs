using App.Contracts.DAL.Repositories;
using App.Domain.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

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