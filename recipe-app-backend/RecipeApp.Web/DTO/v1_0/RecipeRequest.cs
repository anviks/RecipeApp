using Microsoft.AspNetCore.Http;
using RecipeApp.Base.Helpers.Validation.File;
using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Web.DTO.v1_0;

public class RecipeRequest : BaseEntityId
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    [FileSize(0, 10 * 1024 * 1024)]
    [AllowedExtensions([".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg"])]
    public IFormFile? ImageFile { get; set; } = default!;

    public List<string> Instructions { get; set; } = default!;
    public int PreparationTime { get; set; }
    public int CookingTime { get; set; }
    public int Servings { get; set; }
    public bool IsVegetarian { get; set; }
    public bool IsVegan { get; set; }
    public bool IsGlutenFree { get; set; }
}