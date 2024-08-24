using System.ComponentModel.DataAnnotations;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Resources.Errors;
using Resource = RecipeApp.Resources.Entities;

namespace RecipeApp.Application.DTO;

public class Category : BaseEntityId
{
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.Category), Name = "Name")]
    public string Name { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.Category), Name = "Description")]
    public string? Description { get; set; }
}