using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecipeApp.Base;
using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Entities;

public class Ingredient : BaseEntityId
{
    [MaxLength(1024)]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } = default!;

    public ICollection<IngredientTypeAssociation>? IngredientTypeAssociations { get; set; }
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
}