using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;
using Base.Contracts.Domain;
using Base.Domain;


namespace App.Domain;

public class Recipe : BaseEntityId
{
    [MaxLength(128)]
    public string Title { get; set; } = default!;
    
    [MaxLength(512)]
    public string Description { get; set; } = default!;
    
    // TODO: update in ERD
    // TODO: remove hacky solution if possible
    public string ImageFileName { get; set; } = default!;
    
    [Column(TypeName = "json")]
    public string Instructions { get; set; } = default!;
    
    public int PrepationTime { get; set; }
    
    public int CookingTime { get; set; }
    
    public int Servings { get; set; }
    
    public bool IsVegetarian { get; set; }
    
    public bool IsVegan { get; set; }
    
    public bool IsGlutenFree { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Guid AuthorUserId { get; set; }
    public AppUser? AuthorUser { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public Guid? UpdatingUserId { get; set; }
    public AppUser? UpdatingUser { get; set; }
    
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    
    public ICollection<RecipeCategory>? RecipeCategories { get; set; }
    
    public ICollection<Review>? Reviews { get; set; }
}