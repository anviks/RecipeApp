using System.ComponentModel.DataAnnotations;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Resources.Errors;
using Resource = RecipeApp.Resources.Entities;

namespace RecipeApp.Application.DTO;

public class IngredientType : BaseEntityId
{
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.IngredientType), Name = "Name")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.IngredientType), Name = "Description")]
    public string Description { get; set; } = default!;
}