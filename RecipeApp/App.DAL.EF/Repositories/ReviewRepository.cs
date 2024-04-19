using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ReviewRepository : BaseEntityRepository<Review, Review, AppDbContext>, IReviewRepository
{
    public ReviewRepository(AppDbContext dbContext) : base(dbContext, new DalDummyMapper<Review, Review>())
    {
    }
}