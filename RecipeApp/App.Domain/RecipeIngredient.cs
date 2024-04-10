using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class RecipeIngredient : BaseEntityId
{
    public Guid RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
    
    public Guid IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
    
    // TODO: update in ERD schema
    public Guid UnitId { get; set; }
    public Unit? Unit { get; set; }
    
    // e.g. "slice" in "1 slice of bread" 
    public string? CustomUnit { get; set; }
    
    // TODO: update in ERD schema
    public float Quantity { get; set; }
    
    // To add a baby carrot as an ingredient to a recipe, the user would select the ingredient "carrot" and add a modifier "baby"
    [MaxLength(128)]
    // TODO: update in ERD schema
    public string? IngredientModifier { get; set; } = default!;
}