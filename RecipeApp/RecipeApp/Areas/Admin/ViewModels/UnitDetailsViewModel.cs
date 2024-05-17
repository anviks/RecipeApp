using System.ComponentModel.DataAnnotations;
using App.BLL.DTO;
using AppResource = App.Resources.App.BLL.DTO;

namespace RecipeApp.Areas.Admin.ViewModels;

public class UnitDetailsViewModel
{
    public Unit Unit { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.IngredientType), Name = "IngredientTypeSingular")]
    public string IngredientTypeName { get; set; } = default!;
}