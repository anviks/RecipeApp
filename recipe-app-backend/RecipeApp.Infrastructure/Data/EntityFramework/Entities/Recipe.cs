using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecipeApp.Base;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Data.EntityFramework.Entities.Identity;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Entities;

public class Recipe : BaseEntityId
{
    [MaxLength(2048)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(RecipeApp.Resources.Entities.Recipe), Name = nameof(Title))]
    public LangStr Title { get; set; } = default!;
    
    [MaxLength(512)]
    [Display(ResourceType = typeof(RecipeApp.Resources.Entities.Recipe), Name = nameof(Description))]
    public string Description { get; set; } = default!;
    
    [MaxLength(512)]
    public string ImageFileUrl { get; set; } = default!;
    
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(RecipeApp.Resources.Entities.Recipe), Name = nameof(Instructions))]
    public List<string> Instructions { get; set; } = default!;
    
    [Display(ResourceType = typeof(RecipeApp.Resources.Entities.Recipe), Name = nameof(PreparationTime))]
    public int PreparationTime { get; set; }
    
    [Display(ResourceType = typeof(RecipeApp.Resources.Entities.Recipe), Name = nameof(CookingTime))]
    public int CookingTime { get; set; }
    
    [Display(ResourceType = typeof(RecipeApp.Resources.Entities.Recipe), Name = nameof(Servings))]
    public int Servings { get; set; }
    
    [Display(ResourceType = typeof(RecipeApp.Resources.Entities.Recipe), Name = nameof(IsVegetarian))]
    public bool IsVegetarian { get; set; }
    
    [Display(ResourceType = typeof(RecipeApp.Resources.Entities.Recipe), Name = nameof(IsVegan))]
    public bool IsVegan { get; set; }
    
    [Display(ResourceType = typeof(RecipeApp.Resources.Entities.Recipe), Name = nameof(IsGlutenFree))]
    public bool IsGlutenFree { get; set; }
    
    [Display(ResourceType = typeof(RecipeApp.Resources.Entities.Recipe), Name = nameof(CreatedAt))]
    public DateTime CreatedAt { get; set; }
    
    public Guid AuthorUserId { get; set; }
    [Display(ResourceType = typeof(RecipeApp.Resources.Entities.Recipe), Name = nameof(AuthorUser))]
    public AppUser? AuthorUser { get; set; }
    
    [Display(ResourceType = typeof(RecipeApp.Resources.Entities.Recipe), Name = nameof(UpdatedAt))]
    public DateTime? UpdatedAt { get; set; }
    
    public Guid? UpdatingUserId { get; set; }
    [Display(ResourceType = typeof(RecipeApp.Resources.Entities.Recipe), Name = nameof(UpdatingUser))]
    public AppUser? UpdatingUser { get; set; }
    
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    public ICollection<RecipeCategory>? RecipeCategories { get; set; }
    public ICollection<Review>? Reviews { get; set; }
}