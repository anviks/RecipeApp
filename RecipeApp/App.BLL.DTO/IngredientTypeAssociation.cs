using System.ComponentModel.DataAnnotations;
using Base.Domain;
using AppResource = App.Resources.App.BLL.DTO;

namespace App.BLL.DTO;

public class IngredientTypeAssociation : BaseEntityId
{
    [Display(ResourceType = typeof(AppResource.Ingredient), Name = "IngredientSingular")]
    public Guid IngredientId { get; set; }

    [Display(ResourceType = typeof(AppResource.IngredientType), Name = "IngredientTypeSingular")]
    public Guid IngredientTypeId { get; set; }
}