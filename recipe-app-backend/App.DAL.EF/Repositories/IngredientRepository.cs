using App.Contracts.DAL.Repositories;
using DAL_DTO = App.DAL.DTO;
using AutoMapper;
using Base.DAL.EF;
using Base.Domain;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class IngredientRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.Ingredient, DAL_DTO.Ingredient, AppDbContext>(
            dbContext,
            new EntityMapper<Domain.Ingredient, DAL_DTO.Ingredient>(mapper)),
        IIngredientRepository
{
    protected override IQueryable<Domain.Ingredient> GetQuery(bool tracking = false)
    {
        var queryable = base.GetQuery(tracking)
            .Include(ingredient => ingredient.IngredientTypeAssociations); 
        return queryable;
    }
    
    public override DAL_DTO.Ingredient Update(DAL_DTO.Ingredient entity)
    {
        Domain.Ingredient ingredient = Mapper.Map(entity)!;
        Domain.Ingredient existingIngredient = DbSet.AsNoTracking().First(i => i.Id == ingredient.Id);
        
        LangStr name = ingredient.Name;
        ingredient.Name = existingIngredient.Name;
        ingredient.Name.SetTranslation(name);
        
        var entry = DbContext.Update(ingredient);
        return Mapper.Map(entry.Entity)!;
    }
    
    public override void UpdateRange(IEnumerable<DAL_DTO.Ingredient> entities)
    {
        throw new NotImplementedException();
    }
}