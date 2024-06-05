namespace Base.DAL.Contracts;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
