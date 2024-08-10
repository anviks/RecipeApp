using Base.Contracts.DAL;
using BLL_DTO = App.BLL.DTO;

namespace App.Contracts.BLL.Services;

public interface IIngredientTypeAssociationService : IEntityRepository<BLL_DTO.IngredientTypeAssociation>
{
    
}