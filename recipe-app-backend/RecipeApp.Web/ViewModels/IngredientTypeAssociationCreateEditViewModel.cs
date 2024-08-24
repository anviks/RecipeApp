using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Application.DTO;

namespace RecipeApp.Web.ViewModels;

public class IngredientTypeAssociationCreateEditViewModel
{
    public IngredientTypeAssociation IngredientTypeAssociation { get; set; } = default!;
    public SelectList? IngredientSelectList { get; set; }
    public SelectList? IngredientTypeSelectList { get; set; }
}