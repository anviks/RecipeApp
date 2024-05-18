using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Category : BaseEntityId
{
    [MaxLength(2048)]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } = default!;

    [MaxLength(4096)]
    [Column(TypeName = "jsonb")]
    // TODO: update in ERD schema
    public LangStr? Description { get; set; }

    // TODO: update in ERD schema
    // public short BroadnessIndex { get; set; }

    public ICollection<RecipeCategory>? RecipeCategories { get; set; }
}