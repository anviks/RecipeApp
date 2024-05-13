using App.BLL.DTO;

namespace RecipeApp.Areas.Admin.ViewModels;

public class UnitDetailsViewModel
{
    public Unit Unit { get; set; } = default!;
    public string IngredientTypeName { get; set; } = default!;
}