using System.ComponentModel.DataAnnotations;
using RecipeApp.Application.DTO;
using Resource = RecipeApp.Resources.Entities;

namespace RecipeApp.Web.ViewModels;

public class ReviewDetailsViewModel
{
    public ReviewResponse ReviewResponse { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "RecipeSingular")]
    public string RecipeTitle { get; set; } = default!;
}