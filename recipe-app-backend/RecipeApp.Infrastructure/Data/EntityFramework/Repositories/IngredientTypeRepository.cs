using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Base;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Repositories;

public class IngredientTypeRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Entities.IngredientType, DTO.IngredientType, AppDbContext>(dbContext, mapper), IIngredientTypeRepository
{
    public override Task<DTO.IngredientType> UpdateAsync(DTO.IngredientType entity)
    {
        Entities.IngredientType ingredientType = Mapper.Map(entity)!;
        Entities.IngredientType existingIngredientType = DbSet.AsNoTracking().First(it => it.Id == ingredientType.Id);
        
        LangStr name = ingredientType.Name;
        ingredientType.Name = existingIngredientType.Name;
        ingredientType.Name.SetTranslation(name);
        
        LangStr description = ingredientType.Description;
        ingredientType.Description = existingIngredientType.Description;
        ingredientType.Description.SetTranslation(description);
        
        var entry = DbContext.Update(ingredientType);
        return Task.FromResult(Mapper.Map(entry.Entity)!);
    }
}