using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Base;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Repositories;

public class UnitRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Entities.Unit, DTO.Unit, AppDbContext>(dbContext, mapper), 
        IUnitRepository
{
    protected override IQueryable<Entities.Unit> GetQuery(bool tracking = false)
    {
        var query = base.GetQuery(tracking);
        return query.Include(unit => unit.IngredientType);
    }
    
    public override Task<DTO.Unit> UpdateAsync(DTO.Unit entity)
    {
        Entities.Unit unit = Mapper.Map(entity)!;
        Entities.Unit existingUnit = DbSet.AsNoTracking().First(u => u.Id == unit.Id);
        
        LangStr name = unit.Name;
        unit.Name = existingUnit.Name;
        unit.Name.SetTranslation(name);
        
        var entry = DbContext.Update(unit);
        return Task.FromResult(Mapper.Map(entry.Entity)!);
    }
}