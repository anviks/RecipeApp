using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.Contracts.DAL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RecipeRepository : BaseEntityRepository<Recipe, Recipe, AppDbContext>, IRecipeRepository
{
    public RecipeRepository(AppDbContext dbContext) : base(dbContext, new DalDummyMapper<Recipe, Recipe>())
    {
    }
    
    protected override IQueryable<Recipe> GetQuery(bool tracking = false)
    {
        var query = base.GetQuery(tracking);
        return query
            .Include(recipe => recipe.AuthorUser)
            .Include(recipe => recipe.UpdatingUser);
    }
}