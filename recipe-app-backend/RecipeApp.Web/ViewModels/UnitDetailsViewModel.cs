using System.ComponentModel.DataAnnotations;
using RecipeApp.Application.DTO;
using Resource = RecipeApp.Resources.Entities;

namespace RecipeApp.Web.ViewModels;

public class UnitDetailsViewModel
{
    public Unit Unit { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.IngredientType), Name = "IngredientTypeSingular")]
    public string IngredientTypeName { get; set; } = default!;
}