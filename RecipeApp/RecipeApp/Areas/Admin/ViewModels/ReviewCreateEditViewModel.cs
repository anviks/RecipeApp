using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeApp.Areas.Admin.ViewModels;

public class ReviewCreateEditViewModel
{
    public Review Review { get; set; } = default!;
    public SelectList RecipeSelectList { get; set; } = default!;
}