using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class IngredientTypeRepository : BaseEntityRepository<IngredientType, IngredientType, AppDbContext>, IIngredientTypeRepository
{
    public IngredientTypeRepository(AppDbContext dbContext) : base(dbContext, new DalDummyMapper<IngredientType, IngredientType>())
    {
    }
}