using Base.Contracts.BLL;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace Base.BLL;

public abstract class BaseBusinessLogic(IUnitOfWork unitOfWork)
    : IBusinessLogic
{
    public async Task<int> SaveChangesAsync()
    {
        return await unitOfWork.SaveChangesAsync();
    }
}