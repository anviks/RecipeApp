using Base.Contracts.DAL;
using DAL_DTO = App.DAL.DTO;

namespace App.Contracts.DAL.Repositories;

public interface ICategoryRepository : IEntityRepository<DAL_DTO.Category>
{
    
}