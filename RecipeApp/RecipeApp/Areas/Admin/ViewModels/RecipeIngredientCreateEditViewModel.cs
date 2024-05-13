using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeApp.Areas.Admin.ViewModels;

public class RecipeIngredientCreateEditViewModel
{
    public RecipeIngredient RecipeIngredient { get; set; } = default!;
    public SelectList? RecipeSelectList { get; set; }
    public SelectList? IngredientSelectList { get; set; }
    public SelectList? UnitSelectList { get; set; }
}