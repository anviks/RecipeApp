using AutoMapper;
using RecipeApp.Application.Contracts.Services;
using RecipeApp.Application.DTO;
using RecipeApp.Base.Application;
using RecipeApp.Base.Helpers;
using RecipeApp.Infrastructure.Contracts.Data.Repositories;
using DAL = RecipeApp.Infrastructure.Data.DTO;

namespace RecipeApp.Application.Services;

public class ReviewService(
    IReviewRepository repository,
    IMapper mapper)
    : BaseEntityService<DAL.Review, ReviewResponse, IReviewRepository>(repository,
            new EntityMapper<DAL.Review, ReviewResponse>(mapper)),
        IReviewService
{
    private readonly EntityMapper<ReviewRequest, DAL.Review> _mapper = new(mapper);
    
    public ReviewResponse Add(ReviewRequest reviewRequest, Guid userId)
    {
        DAL.Review dalReview = _mapper.Map(reviewRequest)!;
        dalReview.UserId = userId;
        dalReview.CreatedAt = DateTime.Now;
        DAL.Review addedReview = Repository.Add(dalReview);
        return Mapper.Map(addedReview)!;
    }
    
    public async Task<ReviewResponse> UpdateAsync(ReviewRequest reviewRequest)
    {
        DAL.Review existingReview = (await Repository.FindAsync(reviewRequest.Id))!;
        DAL.Review dalReview = _mapper.Map(reviewRequest)!;
        dalReview.Edited = true;
        dalReview.CreatedAt = existingReview.CreatedAt;
        dalReview.UserId = existingReview.UserId;
        dalReview.RecipeId = existingReview.RecipeId;
        DAL.Review updatedReview = Repository.Update(dalReview);
        return Mapper.Map(updatedReview)!;
    }

    public override ReviewResponse Add(ReviewResponse entity)
    {
        throw new MethodAccessException();
    }

    public override ReviewResponse Update(ReviewResponse entity)
    {
        throw new MethodAccessException();
    }
}