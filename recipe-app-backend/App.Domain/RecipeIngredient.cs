using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class RecipeIngredient : BaseEntityId
{
    [MaxLength(512)]
    [Column(TypeName = "jsonb")]
    // e.g. "slice" in "1 slice of bread" 
    public LangStr? CustomUnit { get; set; }
    
    // TODO: update in ERD schema
    public float Quantity { get; set; }
    
    // To add a baby carrot as an ingredient to a recipe, the user would select the ingredient "carrot" and add a modifier "baby"
    [MaxLength(2048)]
    [Column(TypeName = "jsonb")]
    // TODO: update in ERD schema
    public LangStr? IngredientModifier { get; set; } = default!;
    
    // TODO: update in ERD schema
    public Guid? UnitId { get; set; }
    public Unit? Unit { get; set; }
    
    public Guid RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
    
    public Guid IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
}