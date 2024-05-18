using App.Contracts.DAL.Repositories;
using AutoMapper;
using DAL_DTO = App.DAL.DTO;
using Base.DAL.EF;
using Base.Domain;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class IngredientTypeRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.IngredientType, DAL_DTO.IngredientType, AppDbContext>(dbContext,
        new EntityMapper<Domain.IngredientType, DAL_DTO.IngredientType>(mapper)), IIngredientTypeRepository
{
    public override DAL_DTO.IngredientType Update(DAL_DTO.IngredientType entity)
    {
        Domain.IngredientType ingredientType = Mapper.Map(entity)!;
        Domain.IngredientType existingIngredientType = DbSet.AsNoTracking().First(it => it.Id == ingredientType.Id);
        
        LangStr name = ingredientType.Name;
        ingredientType.Name = existingIngredientType.Name;
        ingredientType.Name.SetTranslation(name);
        
        LangStr description = ingredientType.Description;
        ingredientType.Description = existingIngredientType.Description;
        ingredientType.Description.SetTranslation(description);
        
        var entry = DbContext.Update(ingredientType);
        return Mapper.Map(entry.Entity)!;
    }
    
    public override void UpdateRange(IEnumerable<DAL_DTO.IngredientType> entities)
    {
        throw new NotImplementedException();
    }
}