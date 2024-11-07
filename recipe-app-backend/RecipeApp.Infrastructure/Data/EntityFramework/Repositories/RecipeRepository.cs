using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Base;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;
using RecipeApp.Infrastructure.Data.DTO;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Repositories;

public class RecipeRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Entities.Recipe, DTO.Recipe, AppDbContext>(dbContext, mapper),
        IRecipeRepository
{
    public override Task<DTO.Recipe> UpdateAsync(DTO.Recipe entity)
    {
        Entities.Recipe recipe = Mapper.Map(entity)!;
        Entities.Recipe existingRecipe = DbSet.AsNoTracking().First(r => r.Id == recipe.Id);
        
        LangStr title = recipe.Title;
        recipe.Title = existingRecipe.Title;
        recipe.Title.SetTranslation(title);
        
        var entry = DbContext.Update(recipe);
        return Task.FromResult(Mapper.Map(entry.Entity)!);
    }

    public async Task<Recipe?> GetByIdDetailedAsync(Guid id)
    {
        Entities.Recipe? recipe = await GetQuery()
            .Include(r => r.AuthorUser)
            .Include(r => r.UpdatingUser)
            .Include(r => r.RecipeIngredients)!
            .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.RecipeCategories)!
            .ThenInclude(rc => rc.Category)
            .FirstOrDefaultAsync(e => e.Id == id);
        return Mapper.Map(recipe);
    }

    public async Task<IEnumerable<Recipe>> GetAllDetailedAsync()
    {
        IEnumerable<Entities.Recipe> recipes = await GetQuery()
            .Include(r => r.AuthorUser)
            .Include(r => r.UpdatingUser)
            .Include(r => r.RecipeIngredients)!
            .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.RecipeCategories)!
            .ThenInclude(rc => rc.Category)
            .ToListAsync();
        return recipes.Select(Mapper.Map)!;
    }
}