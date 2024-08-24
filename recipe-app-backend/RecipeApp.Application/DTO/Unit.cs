using System.ComponentModel.DataAnnotations;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Resources.Errors;
using Resource = RecipeApp.Resources.Entities;

namespace RecipeApp.Application.DTO;

public class Unit : BaseEntityId
{
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.Unit), Name = "Name")]
    public string Name { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.Unit), Name = "Abbreviation")]
    public string? Abbreviation { get; set; }
    
    [Display(ResourceType = typeof(Resource.Unit), Name = "UnitMultiplier")]
    public float? UnitMultiplier { get; set; }
    
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.IngredientType), Name = "IngredientTypeSingular")]
    public Guid IngredientTypeId { get; set; }
}