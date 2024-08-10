using System.ComponentModel.DataAnnotations;
using Base.Domain;
using AppResource = App.Resources.App.BLL.DTO;
using Base.Resources;

namespace App.BLL.DTO;

public class Ingredient : BaseEntityId
{
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(AppResource.Ingredient), Name = "Name")]
    public string Name { get; set; } = default!;

    public ICollection<IngredientTypeAssociation>? IngredientTypeAssociations { get; set; }
}