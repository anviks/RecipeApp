using System.ComponentModel.DataAnnotations;
using BLL_DTO = App.BLL.DTO.Identity;
using Base.Domain;
using AppResource = App.Resources.App.BLL.DTO;

namespace App.BLL.DTO;

public class RecipeResponse : BaseEntityId
{
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "Title")]
    public string Title { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "Description")]
    public string Description { get; set; } = default!;
    
    public string ImageFileUrl { get; set; } = default!;
    
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
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "CreatedAt")]
    public DateTime CreatedAt { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "AuthorUser")]
    public BLL_DTO.AppUser AuthorUser { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "UpdatedAt")]
    public DateTime? UpdatedAt { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "UpdatingUser")]
    public BLL_DTO.AppUser? UpdatingUser { get; set; }
    
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    
    public ICollection<RecipeCategory>? RecipeCategories { get; set; }
}