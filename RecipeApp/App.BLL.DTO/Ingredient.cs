using Base.Domain;

namespace App.BLL.DTO;

public class Ingredient : BaseEntityId
{
    public string Name { get; set; } = default!;
    public ICollection<IngredientTypeAssociation>? IngredientTypeAssociations { get; set; }
}