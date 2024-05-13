using App.BLL.DTO;

namespace RecipeApp.Areas.Admin.ViewModels;

public class RecipeIngredientDetailsViewModel
{
    public RecipeIngredient RecipeIngredient { get; set; } = default!;
    public string RecipeName { get; set; } = default!;
    public string IngredientName { get; set; } = default!;
    public string UnitName { get; set; } = default!;
}