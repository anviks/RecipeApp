using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class IngredientRepository : BaseEntityRepository<Ingredient, Ingredient, AppDbContext>, IIngredientRepository
{
    public IngredientRepository(AppDbContext dbContext) : base(dbContext, new DalDummyMapper<Ingredient, Ingredient>())
    {
    }
}