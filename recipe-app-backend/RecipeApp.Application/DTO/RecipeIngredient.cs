using System.ComponentModel.DataAnnotations;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Resources.Errors;
using Resource = RecipeApp.Resources.Entities;

namespace RecipeApp.Application.DTO;

public class RecipeIngredient : BaseEntityId
{
    [Display(ResourceType = typeof(Resource.RecipeIngredient), Name = "CustomUnit")]
    public string? CustomUnit { get; set; }
 
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.RecipeIngredient), Name = "Quantity")]
    public float Quantity { get; set; }
    
    [Display(ResourceType = typeof(Resource.RecipeIngredient), Name = "IngredientModifier")]
    public string? IngredientModifier { get; set; }
    
    [Display(ResourceType = typeof(Resource.Unit), Name = "UnitSingular")]
    public Guid? UnitId { get; set; }
    
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.Recipe), Name = "RecipeSingular")]
    public Guid RecipeId { get; set; }
    
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.Ingredient), Name = "IngredientSingular")]
    public Guid IngredientId { get; set; }
}