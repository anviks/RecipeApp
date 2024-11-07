using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Base.Helpers;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Repositories;

public class ReviewRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Entities.Review, DTO.Review, AppDbContext>(dbContext, mapper),
        IReviewRepository
{
    protected override IQueryable<Entities.Review> GetQuery(bool tracking = false)
    {
        var query = base.GetQuery(tracking);
        query = query.Include(review => review.User).Include(review => review.Recipe);
        return query;
    }
}