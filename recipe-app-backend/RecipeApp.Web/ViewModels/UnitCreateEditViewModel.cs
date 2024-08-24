using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Application.DTO;

namespace RecipeApp.Web.ViewModels;

public class UnitCreateEditViewModel
{
    public Unit Unit { get; set; } = default!;
    public SelectList? IngredientTypeSelectList { get; set; } = default!;
}