using RecipeApp.Base.Contracts.Application;
using RecipeApp.Base.Contracts.Infrastructure.Data;

namespace RecipeApp.Base.Application;

public abstract class BaseBusinessLogic(IUnitOfWork unitOfWork)
    : IBusinessLogic
{
    public async Task<int> SaveChangesAsync()
    {
        return await unitOfWork.SaveChangesAsync();
    }
}