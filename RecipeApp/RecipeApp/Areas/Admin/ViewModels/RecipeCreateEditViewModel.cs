using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Validation.File;

namespace RecipeApp.Areas.Admin.ViewModels;

public class RecipeCreateEditViewModel
{
    public Recipe Recipe { get; set; } = default!;
    public SelectList? AuthorUserSelectList { get; set; }
    public SelectList? UpdatingUserSelectList { get; set; }
    [FileSize(0, 10 * 1024 * 1024)]
    [AllowedExtensions([".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg"])]
    // [Required]
    public IFormFile? RecipeImage { get; set; } = default!;
}