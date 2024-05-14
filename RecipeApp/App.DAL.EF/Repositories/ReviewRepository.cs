using App.Contracts.DAL.Repositories;
using AutoMapper;
using DAL_DTO = App.DAL.DTO;
using Base.DAL.EF;
using Helpers;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ReviewRepository(AppDbContext dbContext, IMapper mapper)
    : BaseEntityRepository<Domain.Review, DAL_DTO.Review, AppDbContext>(dbContext,
            new EntityMapper<Domain.Review, DAL_DTO.Review>(mapper)),
        IReviewRepository
{
    protected override IQueryable<Domain.Review> GetQuery(bool tracking = false)
    {
        var query = base.GetQuery(tracking);
        query = query.Include(review => review.User).Include(review => review.Recipe);
        return query;
    }
}