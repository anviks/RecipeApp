using System.ComponentModel.DataAnnotations;
using App.BLL.DTO;
using AppResource = App.Resources.App.BLL.DTO;

namespace RecipeApp.Areas.Admin.ViewModels;

public class RecipeCategoryDetailsViewModel
{
    public RecipeCategory RecipeCategory { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "RecipeSingular")]
    public string RecipeName { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.Category), Name = "CategorySingular")]
    public string CategoryName { get; set; } = default!;
}