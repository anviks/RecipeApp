using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Base;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Repositories;

public class RecipeIngredientRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Entities.RecipeIngredient, DTO.RecipeIngredient, AppDbContext>(dbContext, mapper), 
        IRecipeIngredientRepository
{
    public override Task<DTO.RecipeIngredient> UpdateAsync(DTO.RecipeIngredient entity)
    {
        Entities.RecipeIngredient recipeIngredient = Mapper.Map(entity)!;
        Entities.RecipeIngredient existingRecipeIngredient = DbSet.AsNoTracking().First(ri => ri.Id == recipeIngredient.Id);
        LangStr? customUnit = recipeIngredient.CustomUnit;
        recipeIngredient.CustomUnit = existingRecipeIngredient.CustomUnit;
        recipeIngredient.CustomUnit?.SetTranslation(customUnit);
        
        LangStr? ingredientModifier = recipeIngredient.IngredientModifier;
        recipeIngredient.IngredientModifier = existingRecipeIngredient.IngredientModifier;
        recipeIngredient.IngredientModifier?.SetTranslation(ingredientModifier);
        
        var entry = DbContext.Update(recipeIngredient);
        return Task.FromResult(Mapper.Map(entry.Entity)!);
    }
}