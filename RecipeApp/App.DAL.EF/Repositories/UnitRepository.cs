using App.Contracts.DAL.Repositories;
using AutoMapper;
using DAL_DTO = App.DAL.DTO;
using Base.DAL.EF;
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
}