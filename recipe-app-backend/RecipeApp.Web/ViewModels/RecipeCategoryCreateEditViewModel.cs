using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Application.DTO;

namespace RecipeApp.Web.ViewModels;

public class RecipeCategoryCreateEditViewModel
{
    public RecipeCategory RecipeCategory { get; set; } = default!;
    public SelectList? RecipeSelectList { get; set; }
    public SelectList? CategorySelectList { get; set; }
}