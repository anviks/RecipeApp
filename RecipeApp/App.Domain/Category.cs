using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Category : BaseEntityId
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    [MaxLength(512)]
    // TODO: update in ERD schema
    public string? Description { get; set; }
    
    // TODO: update in ERD schema
    // public short BroadnessIndex { get; set; }
    
    public ICollection<RecipeCategory>? RecipeCategories { get; set; }
}