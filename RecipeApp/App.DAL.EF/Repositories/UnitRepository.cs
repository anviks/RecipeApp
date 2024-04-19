using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.Contracts.DAL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace App.DAL.EF.Repositories;

public class UnitRepository : BaseEntityRepository<Unit, Unit, AppDbContext>, IUnitRepository
{
    public UnitRepository(AppDbContext dbContext) : base(dbContext, new DalDummyMapper<Unit, Unit>())
    {
    }

    protected override IQueryable<Unit> GetQuery(bool tracking = false)
    {
        var query = base.GetQuery(tracking);
        return query.Include(unit => unit.IngredientType);
    }
}