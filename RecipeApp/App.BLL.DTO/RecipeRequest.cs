using System.ComponentModel.DataAnnotations;
using AppResource = App.Resources.App.BLL.DTO;
using Base.Domain;
using Helpers.Validation.File;
using Microsoft.AspNetCore.Http;

namespace App.BLL.DTO;

public class RecipeRequest : BaseEntityId
{
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "Title")]
    public string Title { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "Description")]
    public string Description { get; set; } = default!;
    
    [FileSize(0, 10 * 1024 * 1024)]
    [AllowedExtensions([".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg"])]
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "FoodPicture")]
    public IFormFile? ImageFile { get; set; }

    [Display(ResourceType = typeof(AppResource.Recipe), Name = "Instructions")]
    public List<string> Instructions { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "PreparationTime")]
    public int PreparationTime { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "CookingTime")]
    public int CookingTime { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "Servings")]
    public int Servings { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "IsVegetarian")]
    public bool IsVegetarian { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "IsVegan")]
    public bool IsVegan { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "IsGlutenFree")]
    public bool IsGlutenFree { get; set; }
}