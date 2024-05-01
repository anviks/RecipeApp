using App.Contracts.DAL.Repositories;
using App.Domain;
using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace App.DAL.EF.Repositories;

public class UnitRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Unit, Unit, AppDbContext>(dbContext, new DalDomainMapper<Unit, Unit>(mapper)), IUnitRepository
{
    protected override IQueryable<Unit> GetQuery(bool tracking = false)
    {
        var query = base.GetQuery(tracking);
        return query.Include(unit => unit.IngredientType);
    }
}