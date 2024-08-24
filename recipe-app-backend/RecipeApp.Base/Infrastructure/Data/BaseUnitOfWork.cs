using Microsoft.EntityFrameworkCore;
using RecipeApp.Base.Contracts.Infrastructure.Data;

namespace RecipeApp.Base.Infrastructure.Data;

public abstract class BaseUnitOfWork<TAppDbContext>(TAppDbContext dbContext) : IUnitOfWork
    where TAppDbContext : DbContext
{
    protected readonly TAppDbContext UowDbContext = dbContext;

    public async Task<int> SaveChangesAsync()
    {
        return await UowDbContext.SaveChangesAsync();
    }
}