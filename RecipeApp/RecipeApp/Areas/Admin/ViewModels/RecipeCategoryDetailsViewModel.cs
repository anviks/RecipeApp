using App.BLL.DTO;

namespace RecipeApp.Areas.Admin.ViewModels;

public class RecipeCategoryDetailsViewModel
{
    public RecipeCategory RecipeCategory { get; set; } = default!;
    public string RecipeName { get; set; } = default!;
    public string CategoryName { get; set; } = default!;
}