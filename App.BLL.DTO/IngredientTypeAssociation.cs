using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Base.Resources;
using AppResource = App.Resources.App.BLL.DTO;

namespace App.BLL.DTO;

public class IngredientTypeAssociation : BaseEntityId
{
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(AppResource.Ingredient), Name = "IngredientSingular")]
    public Guid IngredientId { get; set; }

    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(AppResource.IngredientType), Name = "IngredientTypeSingular")]
    public Guid IngredientTypeId { get; set; }
}