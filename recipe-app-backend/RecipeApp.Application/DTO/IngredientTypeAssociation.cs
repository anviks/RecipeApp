using System.ComponentModel.DataAnnotations;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Resources.Errors;
using Resource = RecipeApp.Resources.Entities;

namespace RecipeApp.Application.DTO;

public class IngredientTypeAssociation : BaseEntityId
{
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.Ingredient), Name = "IngredientSingular")]
    public Guid IngredientId { get; set; }

    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.IngredientType), Name = "IngredientTypeSingular")]
    public Guid IngredientTypeId { get; set; }
}