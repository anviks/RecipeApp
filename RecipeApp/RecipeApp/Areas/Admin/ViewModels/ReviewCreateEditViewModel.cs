using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeApp.Areas.Admin.ViewModels;

public class ReviewCreateEditViewModel
{
    public ReviewRequest ReviewRequest { get; set; } = default!;
    public SelectList? RecipeSelectList { get; set; } = default!;
}