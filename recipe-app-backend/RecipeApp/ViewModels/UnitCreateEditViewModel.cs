using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeApp.ViewModels;

public class UnitCreateEditViewModel
{
    public Unit Unit { get; set; } = default!;
    public SelectList? IngredientTypeSelectList { get; set; } = default!;
}