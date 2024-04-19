using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class RecipeCategoryRepository : BaseEntityRepository<RecipeCategory, RecipeCategory, AppDbContext>, IRecipeCategoryRepository
{
    public RecipeCategoryRepository(AppDbContext dbContext) : base(dbContext, new DalDummyMapper<RecipeCategory, RecipeCategory>())
    {
    }
}