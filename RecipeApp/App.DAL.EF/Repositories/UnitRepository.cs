using App.Contracts.DAL.Repositories;
using AutoMapper;
using DAL_DTO = App.DAL.DTO;
using Base.DAL.EF;
using Base.Domain;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UnitRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.Unit, DAL_DTO.Unit, AppDbContext>(dbContext,
        new EntityMapper<Domain.Unit, DAL_DTO.Unit>(mapper)), IUnitRepository
{
    protected override IQueryable<Domain.Unit> GetQuery(bool tracking = false)
    {
        var query = base.GetQuery(tracking);
        return query.Include(unit => unit.IngredientType);
    }
    
    public override DAL_DTO.Unit Update(DAL_DTO.Unit entity)
    {
        Domain.Unit unit = Mapper.Map(entity)!;
        Domain.Unit existingUnit = DbSet.AsNoTracking().First(u => u.Id == unit.Id);
        
        LangStr name = unit.Name;
        unit.Name = existingUnit.Name;
        unit.Name.SetTranslation(name);
        
        var entry = DbContext.Update(unit);
        return Mapper.Map(entry.Entity)!;
    }
    
    public override void UpdateRange(IEnumerable<DAL_DTO.Unit> entities)
    {
        throw new NotImplementedException();
    }
}