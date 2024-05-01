using App.Contracts.DAL.Repositories;
using App.Domain;
using AutoMapper;
using Base.Contracts.DAL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class IngredientTypeAssociationRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<IngredientTypeAssociation, IngredientTypeAssociation, AppDbContext>(dbContext,
            new DalDomainMapper<IngredientTypeAssociation, IngredientTypeAssociation>(mapper)),
        IIngredientTypeAssociationRepository
{
    protected override IQueryable<IngredientTypeAssociation> GetQuery(bool tracking = false)
    {
        var query = base.GetQuery(tracking);
        return query
            .Include(association => association.IngredientType)
            .Include(association => association.Ingredient);
    }
}