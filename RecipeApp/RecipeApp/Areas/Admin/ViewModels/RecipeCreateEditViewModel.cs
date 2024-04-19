using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using RecipeApp.Validation.File;

namespace RecipeApp.Areas.Admin.ViewModels;

public class RecipeCreateEditViewModel
{
    public Recipe Recipe { get; set; } = default!;
    public SelectList? AuthorUserSelectList { get; set; }
    public SelectList? UpdatingUserSelectList { get; set; }
    [FileSize(0, 1048576)]
    [AllowedExtensions([".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg"])]
    public IFormFile RecipeImage { get; set; } = default!;
}