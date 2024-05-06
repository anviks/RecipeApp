using BLL_DTO = App.BLL.DTO;
using RecipeApp.Validation.File;

namespace RecipeApp.Areas.Admin.ViewModels;

public class RecipeCreateEditViewModel
{
    public BLL_DTO.RecipeRequest RecipeRequest { get; set; } = default!;
    [FileSize(0, 10 * 1024 * 1024)]
    [AllowedExtensions([".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg"])]
    public IFormFile RecipeImage { get; set; } = default!;
}