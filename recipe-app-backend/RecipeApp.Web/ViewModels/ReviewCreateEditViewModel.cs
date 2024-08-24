using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Application.DTO;

namespace RecipeApp.Web.ViewModels;

public class ReviewCreateEditViewModel
{
    public ReviewRequest ReviewRequest { get; set; } = default!;
    public SelectList? RecipeSelectList { get; set; } = default!;
}