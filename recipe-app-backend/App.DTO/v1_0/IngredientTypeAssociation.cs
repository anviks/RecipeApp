using Base.Domain;

namespace App.DTO.v1_0;

public class IngredientTypeAssociation : BaseEntityId
{
    public Guid IngredientId { get; set; }
    public Guid IngredientTypeId { get; set; }
}