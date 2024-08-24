using AutoMapper;
using RecipeApp.Base.Helpers;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Repositories;

public class IngredientTypeAssociationRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Entities.IngredientTypeAssociation, DTO.IngredientTypeAssociation, AppDbContext>(dbContext,
            new EntityMapper<Entities.IngredientTypeAssociation, DTO.IngredientTypeAssociation>(mapper)),
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