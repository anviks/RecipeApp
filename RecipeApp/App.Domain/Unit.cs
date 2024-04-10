using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Unit : BaseEntityId
{
    public Guid IngredientTypeId { get; set; }
    public IngredientType? IngredientType { get; set; }
    
    [MaxLength(64)]
    public string Name { get; set; } = default!;
    
    [MaxLength(16)]
    public string? Abbreviation { get; set; }
    
    // TODO: update in ERD schema
    public float? UnitMultiplier { get; set; }
    
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
}