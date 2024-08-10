using DAL_DTO = App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IUnitRepository : IEntityRepository<DAL_DTO.Unit>
{
    
}