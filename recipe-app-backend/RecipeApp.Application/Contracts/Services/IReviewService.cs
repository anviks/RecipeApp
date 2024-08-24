using RecipeApp.Application.DTO;
using RecipeApp.Base.Contracts.Infrastructure.Data;

namespace RecipeApp.Application.Contracts.Services;

public interface IReviewService : IEntityRepository<ReviewResponse>
{
    ReviewResponse Add(ReviewRequest reviewRequest, Guid userId);
    Task<ReviewResponse> UpdateAsync(ReviewRequest reviewRequest);
}