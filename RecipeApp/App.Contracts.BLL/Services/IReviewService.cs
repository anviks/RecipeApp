using Base.Contracts.DAL;
using BLL_DTO = App.BLL.DTO;

namespace App.Contracts.BLL.Services;

public interface IReviewService : IEntityRepository<BLL_DTO.ReviewResponse>
{
    BLL_DTO.ReviewResponse Add(BLL_DTO.ReviewRequest reviewRequest, Guid userId);
    Task<BLL_DTO.ReviewResponse> UpdateAsync(BLL_DTO.ReviewRequest reviewRequest);
}