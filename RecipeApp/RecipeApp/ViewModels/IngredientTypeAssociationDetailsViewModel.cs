using System.ComponentModel.DataAnnotations;
using App.BLL.DTO;
using AppResource = App.Resources.App.BLL.DTO;

namespace RecipeApp.ViewModels;

public class IngredientTypeAssociationDetailsViewModel
{
    public IngredientTypeAssociation IngredientTypeAssociation { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.Ingredient), Name = "IngredientSingular")]
    public string IngredientName { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.IngredientType), Name = "IngredientTypeSingular")]
    public string IngredientTypeName { get; set; } = default!;
}