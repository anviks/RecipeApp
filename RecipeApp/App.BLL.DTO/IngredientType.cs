using System.ComponentModel.DataAnnotations;
using Base.Domain;
using AppResource = App.Resources.App.BLL.DTO;
using Base.Resources;

namespace App.BLL.DTO;

public class IngredientType : BaseEntityId
{
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(AppResource.IngredientType), Name = "Name")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(AppResource.IngredientType), Name = "Description")]
    public string Description { get; set; } = default!;
}