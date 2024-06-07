using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.Contracts.Repositories;

public interface IRaffleRepository : IEntityRepository<DAL.DTO.Raffle>
{
    public Task<IEnumerable<Raffle>> FindAllWithAccessAsync(bool isAdmin, Guid? companyId);
}