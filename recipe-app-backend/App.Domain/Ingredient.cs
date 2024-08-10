using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Ingredient : BaseEntityId
{
    [MaxLength(1024)]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } = default!;

    public ICollection<IngredientTypeAssociation>? IngredientTypeAssociations { get; set; }
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
}