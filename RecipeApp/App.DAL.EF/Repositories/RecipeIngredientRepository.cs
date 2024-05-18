using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Base.Domain;
using Helpers;
using Microsoft.EntityFrameworkCore;
using DAL_DTO = App.DAL.DTO;

namespace App.DAL.EF.Repositories;

public class RecipeIngredientRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.RecipeIngredient, DAL_DTO.RecipeIngredient, AppDbContext>(dbContext,
        new EntityMapper<Domain.RecipeIngredient, DAL_DTO.RecipeIngredient>(mapper)), IRecipeIngredientRepository
{
    public override DAL_DTO.RecipeIngredient Update(DAL_DTO.RecipeIngredient entity)
    {
        Domain.RecipeIngredient recipeIngredient = Mapper.Map(entity)!;
        Domain.RecipeIngredient existingRecipeIngredient = DbSet.AsNoTracking().First(ri => ri.Id == recipeIngredient.Id);
        LangStr? customUnit = recipeIngredient.CustomUnit;
        recipeIngredient.CustomUnit = existingRecipeIngredient.CustomUnit;
        recipeIngredient.CustomUnit?.SetTranslation(customUnit);
        
        LangStr? ingredientModifier = recipeIngredient.IngredientModifier;
        recipeIngredient.IngredientModifier = existingRecipeIngredient.IngredientModifier;
        recipeIngredient.IngredientModifier?.SetTranslation(ingredientModifier);
        
        var entry = DbContext.Update(recipeIngredient);
        return Mapper.Map(entry.Entity)!;
    }
    
    public override void UpdateRange(IEnumerable<DAL_DTO.RecipeIngredient> entities)
    {
        throw new NotImplementedException();
    }
}