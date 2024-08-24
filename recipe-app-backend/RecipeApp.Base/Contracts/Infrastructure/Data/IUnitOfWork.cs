namespace RecipeApp.Base.Contracts.Infrastructure.Data;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
