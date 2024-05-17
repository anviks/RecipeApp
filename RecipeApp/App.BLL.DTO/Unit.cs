using System.ComponentModel.DataAnnotations;
using Base.Domain;
using AppResource = App.Resources.App.BLL.DTO;

namespace App.BLL.DTO;

public class Unit : BaseEntityId
{
    [Display(ResourceType = typeof(AppResource.Unit), Name = "Name")]
    public string Name { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.Unit), Name = "Abbreviation")]
    public string? Abbreviation { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Unit), Name = "UnitMultiplier")]
    public float? UnitMultiplier { get; set; }
    
    [Display(ResourceType = typeof(AppResource.IngredientType), Name = "IngredientTypeSingular")]
    public Guid IngredientTypeId { get; set; }
}