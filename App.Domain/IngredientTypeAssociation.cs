using Base.Domain;

namespace App.Domain;

public class IngredientTypeAssociation : BaseEntityId
{
    public Guid IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
    
    public Guid IngredientTypeId { get; set; }
    public IngredientType? IngredientType { get; set; }
}