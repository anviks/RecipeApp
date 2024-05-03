using App.Contracts.DAL.Repositories;
using AutoMapper;
using AppDomain = App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using DAL_DTO = App.DAL.DTO;

namespace App.DAL.EF.Repositories;

public class RecipeRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<AppDomain.Recipe, DAL_DTO.Recipe, AppDbContext>(
            dbContext,
            new DalDomainMapper<AppDomain.Recipe, DAL_DTO.Recipe>(mapper)),
        IRecipeRepository
{
    protected override IQueryable<AppDomain.Recipe> GetQuery(bool tracking = false)
    {
        var query = base.GetQuery(tracking);
        return query
            .Include(recipe => recipe.AuthorUser)
            .Include(recipe => recipe.UpdatingUser);
    }
}