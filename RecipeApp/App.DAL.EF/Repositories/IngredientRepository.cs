using App.Contracts.DAL.Repositories;
using DAL_DTO = App.DAL.DTO;
using AutoMapper;
using Base.DAL.EF;
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
}