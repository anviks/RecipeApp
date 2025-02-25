using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Base;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Repositories;

public class IngredientRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Entities.Ingredient, DTO.Ingredient, AppDbContext>(dbContext, mapper),
        IIngredientRepository
{
    protected override IQueryable<Entities.Ingredient> GetQuery(bool tracking = false)
    {
        var queryable = base.GetQuery(tracking)
            .Include(ingredient => ingredient.IngredientTypeAssociations); 
        return queryable;
    }
    
    public override Task<DTO.Ingredient> UpdateAsync(DTO.Ingredient entity)
    {
        Entities.Ingredient ingredient = Mapper.Map(entity)!;
        Entities.Ingredient existingIngredient = DbSet.AsNoTracking().First(i => i.Id == ingredient.Id);
        
        LangStr name = ingredient.Name;
        ingredient.Name = existingIngredient.Name;
        ingredient.Name.SetTranslation(name);
        
        var entry = DbContext.Update(ingredient);
        return Task.FromResult(Mapper.Map(entry.Entity)!);
    }
}