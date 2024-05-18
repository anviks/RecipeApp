using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class IngredientType : BaseEntityId
{
    [MaxLength(1024)]
    [Column(TypeName = "jsonb")]
    // TODO: update in ERD schema
    public LangStr Name { get; set; } = default!;
    
    [MaxLength(4096)]
    [Column(TypeName = "jsonb")]
    public LangStr Description { get; set; } = default!;
    
    public ICollection<Unit>? Units { get; set; }
    public ICollection<IngredientTypeAssociation>? IngredientTypeAssociations { get; set; }
}