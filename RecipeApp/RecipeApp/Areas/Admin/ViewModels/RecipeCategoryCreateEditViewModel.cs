using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeApp.Areas.Admin.ViewModels;

public class RecipeCategoryCreateEditViewModel
{
    public RecipeCategory RecipeCategory { get; set; } = default!;
    public SelectList? RecipeSelectList { get; set; }
    public SelectList? CategorySelectList { get; set; }
}