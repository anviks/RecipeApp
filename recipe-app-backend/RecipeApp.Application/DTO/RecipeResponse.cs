using System.ComponentModel.DataAnnotations;
using RecipeApp.Application.DTO.Identity;
using RecipeApp.Base.Infrastructure.Data;
using Resource = RecipeApp.Resources.Entities;

namespace RecipeApp.Application.DTO;

using AppUser = AppUser;

public class RecipeResponse : BaseEntityId
{
    [Display(ResourceType = typeof(Resource.Recipe), Name = "Title")]
    public string Title { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "Description")]
    public string Description { get; set; } = default!;
    
    public string ImageFileUrl { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "Instructions")]
    public List<string> Instructions { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "PreparationTime")]
    public int PreparationTime { get; set; }
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "CookingTime")]
    public int CookingTime { get; set; }
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "Servings")]
    public int Servings { get; set; }
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "IsVegetarian")]
    public bool IsVegetarian { get; set; }
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "IsVegan")]
    public bool IsVegan { get; set; }
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "IsGlutenFree")]
    public bool IsGlutenFree { get; set; }
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "CreatedAt")]
    public DateTime CreatedAt { get; set; }
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "AuthorUser")]
    public AppUser AuthorUser { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "UpdatedAt")]
    public DateTime? UpdatedAt { get; set; }
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "UpdatingUser")]
    public AppUser? UpdatingUser { get; set; }
    
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    
    public ICollection<RecipeCategory>? RecipeCategories { get; set; }
}