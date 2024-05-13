using Base.Contracts.DAL;
using BLL_DTO = App.BLL.DTO;

namespace App.Contracts.BLL.Services;

public interface IReviewService : IEntityRepository<BLL_DTO.Review>
{
    
}