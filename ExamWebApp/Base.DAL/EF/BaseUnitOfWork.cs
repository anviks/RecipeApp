using Base.DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public abstract class BaseUnitOfWork<TAppDbContext>(TAppDbContext dbContext) : IUnitOfWork
    where TAppDbContext : DbContext
{
    protected readonly TAppDbContext UowDbContext = dbContext;

    public async Task<int> SaveChangesAsync()
    {
        return await UowDbContext.SaveChangesAsync();
    }
}