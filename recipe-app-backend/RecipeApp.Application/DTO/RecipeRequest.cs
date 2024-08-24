using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using RecipeApp.Base.Helpers.Validation.File;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Resources.Errors;
using Resource = RecipeApp.Resources.Entities;

namespace RecipeApp.Application.DTO;

public class RecipeRequest : BaseEntityId
{
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.Recipe), Name = "Title")]
    public string Title { get; set; } = default!;
    
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.Recipe), Name = "Description")]
    public string Description { get; set; } = default!;
    
    [FileSize(0, 10 * 1024 * 1024)]
    [AllowedExtensions([".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg"])]
    [Display(ResourceType = typeof(Resource.Recipe), Name = "FoodPicture")]
    public IFormFile? ImageFile { get; set; }

    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.Recipe), Name = "Instructions")]
    public List<string> Instructions { get; set; } = default!;
    
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.Recipe), Name = "PreparationTime")]
    public int PreparationTime { get; set; }
    
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.Recipe), Name = "CookingTime")]
    public int CookingTime { get; set; }
    
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.Recipe), Name = "Servings")]
    public int Servings { get; set; }
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "IsVegetarian")]
    public bool IsVegetarian { get; set; }
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "IsVegan")]
    public bool IsVegan { get; set; }
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "IsGlutenFree")]
    public bool IsGlutenFree { get; set; }
}