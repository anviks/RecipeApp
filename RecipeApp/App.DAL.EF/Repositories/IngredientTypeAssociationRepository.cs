using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.Contracts.DAL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class IngredientTypeAssociationRepository : BaseEntityRepository<IngredientTypeAssociation, IngredientTypeAssociation, AppDbContext>, IIngredientTypeAssociationRepository
{
    public IngredientTypeAssociationRepository(AppDbContext dbContext) : base(dbContext, new DalDummyMapper<IngredientTypeAssociation, IngredientTypeAssociation>())
    {
    }

    protected override IQueryable<IngredientTypeAssociation> GetQuery(bool tracking = false)
    {
        var query = base.GetQuery(tracking);
        return query
            .Include(association => association.IngredientType)
            .Include(association => association.Ingredient);
    }
}