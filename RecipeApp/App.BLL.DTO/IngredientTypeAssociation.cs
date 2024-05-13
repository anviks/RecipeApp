using Base.Domain;

namespace App.BLL.DTO;

public class IngredientTypeAssociation : BaseEntityId
{
    public Guid IngredientId { get; set; }
    public Guid IngredientTypeId { get; set; }
}