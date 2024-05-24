using System.ComponentModel.DataAnnotations;
using App.BLL.DTO;
using AppResource = App.Resources.App.BLL.DTO;

namespace RecipeApp.ViewModels;

public class ReviewDetailsViewModel
{
    public ReviewResponse ReviewResponse { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "RecipeSingular")]
    public string RecipeTitle { get; set; } = default!;
}