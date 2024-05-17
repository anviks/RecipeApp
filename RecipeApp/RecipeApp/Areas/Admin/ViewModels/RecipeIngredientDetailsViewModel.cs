using System.ComponentModel.DataAnnotations;
using App.BLL.DTO;
using AppResource = App.Resources.App.BLL.DTO;

namespace RecipeApp.Areas.Admin.ViewModels;

public class RecipeIngredientDetailsViewModel
{
    public RecipeIngredient RecipeIngredient { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "RecipeSingular")]
    public string RecipeName { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.Ingredient), Name = "IngredientSingular")]
    public string IngredientName { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.Unit), Name = "UnitSingular")]
    public string UnitName { get; set; } = default!;
}