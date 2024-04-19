using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IIngredientRepository : IEntityRepository<Ingredient>
{
}