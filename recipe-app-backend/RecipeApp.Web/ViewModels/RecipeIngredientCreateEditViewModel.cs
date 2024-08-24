using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Application.DTO;

namespace RecipeApp.Web.ViewModels;

public class RecipeIngredientCreateEditViewModel
{
    public RecipeIngredient RecipeIngredient { get; set; } = default!;
    public SelectList? RecipeSelectList { get; set; }
    public SelectList? IngredientSelectList { get; set; }
    public SelectList? UnitSelectList { get; set; }
}