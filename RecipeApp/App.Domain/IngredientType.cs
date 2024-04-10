using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class IngredientType : BaseEntityId
{
    [MaxLength(64)]
    // TODO: update in ERD schema
    public string Name { get; set; } = default!;
    
    [MaxLength(512)]
    public string Description { get; set; } = default!;
    
    public ICollection<Unit>? Units { get; set; }
    
    public ICollection<IngredientTypeAssociation>? IngredientTypeAssociations { get; set; }
}