using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Base;
using RecipeApp.Base.Helpers;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Repositories;

public class RecipeRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Entities.Recipe, DTO.Recipe, AppDbContext>(
            dbContext,
            new EntityMapper<Entities.Recipe, DTO.Recipe>(mapper)),
        IRecipeRepository
{
    protected override IQueryable<Entities.Recipe> GetQuery(bool tracking = false)
    {
        var query = base.GetQuery(tracking);
        return query
            .Include(recipe => recipe.AuthorUser)
            .Include(recipe => recipe.UpdatingUser)
            .Include(recipe => recipe.RecipeCategories)
            .Include(recipe => recipe.RecipeIngredients);
    }
    
    public override DTO.Recipe Update(DTO.Recipe entity)
    {
        Entities.Recipe recipe = Mapper.Map(entity)!;
        Entities.Recipe existingRecipe = DbSet.AsNoTracking().First(r => r.Id == recipe.Id);
        
        LangStr title = recipe.Title;
        recipe.Title = existingRecipe.Title;
        recipe.Title.SetTranslation(title);
        
        var entry = DbContext.Update(recipe);
        return Mapper.Map(entry.Entity)!;
    }
    
    public override void UpdateRange(IEnumerable<DTO.Recipe> entities)
    {
        List<Entities.Recipe> recipes = entities.Select(Mapper.Map).ToList()!;
        
        foreach (Entities.Recipe recipe in recipes)
        {
            Entities.Recipe existingRecipe = DbSet.AsNoTracking().First(r => r.Id == recipe.Id);
            
            LangStr title = recipe.Title;
            recipe.Title = existingRecipe.Title;
            recipe.Title.SetTranslation(title);
        }
        
        DbContext.UpdateRange(recipes);
    }
}