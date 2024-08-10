using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Base.Domain;
using Helpers;
using Microsoft.EntityFrameworkCore;
using DAL_DTO = App.DAL.DTO;

namespace App.DAL.EF.Repositories;

public class RecipeRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.Recipe, DAL_DTO.Recipe, AppDbContext>(
            dbContext,
            new EntityMapper<Domain.Recipe, DAL_DTO.Recipe>(mapper)),
        IRecipeRepository
{
    protected override IQueryable<Domain.Recipe> GetQuery(bool tracking = false)
    {
        var query = base.GetQuery(tracking);
        return query
            .Include(recipe => recipe.AuthorUser)
            .Include(recipe => recipe.UpdatingUser)
            .Include(recipe => recipe.RecipeCategories)
            .Include(recipe => recipe.RecipeIngredients);
    }
    
    public override DAL_DTO.Recipe Update(DAL_DTO.Recipe entity)
    {
        Domain.Recipe recipe = Mapper.Map(entity)!;
        Domain.Recipe existingRecipe = DbSet.AsNoTracking().First(r => r.Id == recipe.Id);
        
        LangStr title = recipe.Title;
        recipe.Title = existingRecipe.Title;
        recipe.Title.SetTranslation(title);
        
        var entry = DbContext.Update(recipe);
        return Mapper.Map(entry.Entity)!;
    }
    
    public override void UpdateRange(IEnumerable<DAL_DTO.Recipe> entities)
    {
        List<Domain.Recipe> recipes = entities.Select(Mapper.Map).ToList()!;
        
        foreach (Domain.Recipe recipe in recipes)
        {
            Domain.Recipe existingRecipe = DbSet.AsNoTracking().First(r => r.Id == recipe.Id);
            
            LangStr title = recipe.Title;
            recipe.Title = existingRecipe.Title;
            recipe.Title.SetTranslation(title);
        }
        
        DbContext.UpdateRange(recipes);
    }
}