using System.ComponentModel.DataAnnotations;
using RecipeApp.Application.DTO;
using Resource = RecipeApp.Resources.Entities;

namespace RecipeApp.Web.ViewModels;

public class IngredientTypeAssociationDetailsViewModel
{
    public IngredientTypeAssociation IngredientTypeAssociation { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.Ingredient), Name = "IngredientSingular")]
    public string IngredientName { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.IngredientType), Name = "IngredientTypeSingular")]
    public string IngredientTypeName { get; set; } = default!;
}