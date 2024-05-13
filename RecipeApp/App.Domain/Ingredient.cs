using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Ingredient : BaseEntityId
{
    [MaxLength(64)]
    public string Name { get; set; } = default!;
    public ICollection<IngredientTypeAssociation>? IngredientTypeAssociations { get; set; }
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
}