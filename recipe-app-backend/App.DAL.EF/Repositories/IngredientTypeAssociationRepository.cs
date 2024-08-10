using App.Contracts.DAL.Repositories;
using DAL_DTO = App.DAL.DTO;
using AutoMapper;
using Base.DAL.EF;
using Helpers;

namespace App.DAL.EF.Repositories;

public class IngredientTypeAssociationRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.IngredientTypeAssociation, DAL_DTO.IngredientTypeAssociation, AppDbContext>(dbContext,
            new EntityMapper<Domain.IngredientTypeAssociation, DAL_DTO.IngredientTypeAssociation>(mapper)),
        IIngredientTypeAssociationRepository
{
    // protected override IQueryable<AppDomain.IngredientTypeAssociation> GetQuery(bool tracking = false)
    // {
    //     var query = base.GetQuery(tracking);
    //     return query
    //         .Include(association => association.IngredientType)
    //         .Include(association => association.Ingredient);
    // }
}