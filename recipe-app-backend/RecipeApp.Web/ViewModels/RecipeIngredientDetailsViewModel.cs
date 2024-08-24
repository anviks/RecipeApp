using System.ComponentModel.DataAnnotations;
using RecipeApp.Application.DTO;
using Resource = RecipeApp.Resources.Entities;

namespace RecipeApp.Web.ViewModels;

public class RecipeIngredientDetailsViewModel
{
    public RecipeIngredient RecipeIngredient { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "RecipeSingular")]
    public string RecipeName { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.Ingredient), Name = "IngredientSingular")]
    public string IngredientName { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.Unit), Name = "UnitSingular")]
    public string UnitName { get; set; } = default!;
}