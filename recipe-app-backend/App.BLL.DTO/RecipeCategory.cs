using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Base.Resources;
using AppResource = App.Resources.App.BLL.DTO;

namespace App.BLL.DTO;

public class RecipeCategory : BaseEntityId
{
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(AppResource.Category), Name = "CategorySingular")]
    public Guid CategoryId { get; set; }
    
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "RecipeSingular")]
    public Guid RecipeId { get; set; }
}