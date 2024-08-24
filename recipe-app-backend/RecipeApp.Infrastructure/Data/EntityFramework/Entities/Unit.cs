using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecipeApp.Base;
using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Entities;

public class Unit : BaseEntityId
{
    [MaxLength(2048)]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } = default!;
    
    [MaxLength(16)]
    public string? Abbreviation { get; set; }
    
    // TODO: update in ERD schema
    public float? UnitMultiplier { get; set; }
    
    public Guid IngredientTypeId { get; set; }
    public IngredientType? IngredientType { get; set; }
    
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
}