using System.ComponentModel.DataAnnotations;
using RecipeApp.Application.DTO.Identity;
using RecipeApp.Base.Infrastructure.Data;
using Resource = RecipeApp.Resources.Entities;

namespace RecipeApp.Application.DTO;

public class ReviewResponse : BaseEntityId
{
    [Display(ResourceType = typeof(Resource.Review), Name = "Edited")]
    public bool Edited { get; set; }
    
    [Range(1, 10)]
    [Display(ResourceType = typeof(Resource.Review), Name = "Rating")]
    public short Rating { get; set; }
    
    [Display(ResourceType = typeof(Resource.Review), Name = "Comment")]
    public string Comment { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resource.Review), Name = "CreatedAt")]
    public DateTime CreatedAt { get; set; }
    
    [Display(ResourceType = typeof(Resource.Recipe), Name = "RecipeSingular")]
    public Guid RecipeId { get; set; }
    
    [Display(ResourceType = typeof(Resource.Review), Name = "Author")]
    public AppUser User { get; set; } = default!;
}