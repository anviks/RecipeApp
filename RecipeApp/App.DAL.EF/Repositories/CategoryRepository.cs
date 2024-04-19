using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CategoryRepository : BaseEntityRepository<Category, Category, AppDbContext>, ICategoryRepository
{
    public CategoryRepository(AppDbContext dbContext) : base(dbContext, new DalDummyMapper<Category, Category>())
    {
    }
}