using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Helpers;
using Microsoft.EntityFrameworkCore;
using DAL_DTO = App.DAL.DTO;

namespace App.DAL.EF.Repositories;

public class RecipeRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.Recipe, DAL_DTO.Recipe, AppDbContext>(
            dbContext,
            new EntityMapper<Domain.Recipe, DAL_DTO.Recipe>(mapper)),
        IRecipeRepository
{
    protected override IQueryable<Domain.Recipe> GetQuery(bool tracking = false)
    {
        var query = base.GetQuery(tracking);
        return query
            .Include(recipe => recipe.AuthorUser)
            .Include(recipe => recipe.UpdatingUser)
            .Include(recipe => recipe.RecipeCategories)
            .Include(recipe => recipe.RecipeIngredients);
    }
}