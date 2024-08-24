using System.ComponentModel.DataAnnotations;
using RecipeApp.Application.DTO;
using Resource = RecipeApp.Resources.Entities;

namespace RecipeApp.Web.ViewModels;

public class RecipeCategoryDetailsViewModel
{
    public RecipeCategory RecipeCategory { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "RecipeSingular")]
    public string RecipeName { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.Category), Name = "CategorySingular")]
    public string CategoryName { get; set; } = default!;
}