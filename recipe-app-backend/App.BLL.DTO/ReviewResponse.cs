using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using Base.Domain;
using AppResource = App.Resources.App.BLL.DTO;

namespace App.BLL.DTO;

public class ReviewResponse : BaseEntityId
{
    [Display(ResourceType = typeof(AppResource.Review), Name = "Edited")]
    public bool Edited { get; set; }
    
    [Range(1, 10)]
    [Display(ResourceType = typeof(AppResource.Review), Name = "Rating")]
    public short Rating { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Review), Name = "Comment")]
    public string Comment { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.Review), Name = "CreatedAt")]
    public DateTime CreatedAt { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "RecipeSingular")]
    public Guid RecipeId { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Review), Name = "Author")]
    public AppUser User { get; set; } = default!;
}