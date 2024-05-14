using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;
using Helpers;
using DAL_DTO = App.DAL.DTO;
using BLL_DTO = App.BLL.DTO;

namespace App.BLL.Services;

public class ReviewService(
    IUnitOfWork unitOfWork,
    IReviewRepository repository,
    IMapper mapper)
    : BaseEntityService<DAL_DTO.Review, BLL_DTO.ReviewResponse, IReviewRepository>(unitOfWork, repository,
            new EntityMapper<DAL_DTO.Review, BLL_DTO.ReviewResponse>(mapper)),
        IReviewService
{
    private readonly EntityMapper<BLL_DTO.ReviewRequest, DAL_DTO.Review> _mapper = new(mapper);
    
    public BLL_DTO.ReviewResponse Add(BLL_DTO.ReviewRequest reviewRequest, Guid userId)
    {
        DAL_DTO.Review dalReview = _mapper.Map(reviewRequest)!;
        dalReview.UserId = userId;
        dalReview.CreatedAt = DateTime.Now;
        DAL_DTO.Review addedReview = Repository.Add(dalReview);
        return Mapper.Map(addedReview)!;
    }
    
    public async Task<BLL_DTO.ReviewResponse> UpdateAsync(BLL_DTO.ReviewRequest reviewRequest)
    {
        DAL_DTO.Review existingReview = (await Repository.FindAsync(reviewRequest.Id))!;
        DAL_DTO.Review dalReview = _mapper.Map(reviewRequest)!;
        dalReview.Edited = true;
        dalReview.CreatedAt = existingReview.CreatedAt;
        dalReview.UserId = existingReview.UserId;
        dalReview.RecipeId = existingReview.RecipeId;
        DAL_DTO.Review updatedReview = Repository.Update(dalReview);
        return Mapper.Map(updatedReview)!;
    }

    public override BLL_DTO.ReviewResponse Add(BLL_DTO.ReviewResponse entity)
    {
        throw new MethodAccessException();
    }

    public override BLL_DTO.ReviewResponse Update(BLL_DTO.ReviewResponse entity)
    {
        throw new MethodAccessException();
    }
}