using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class RecipeIngredientRepository : BaseEntityRepository<RecipeIngredient, RecipeIngredient, AppDbContext>, IRecipeIngredientRepository
{
    public RecipeIngredientRepository(AppDbContext dbContext) : base(dbContext, new DalDummyMapper<RecipeIngredient, RecipeIngredient>())
    {
    }
}