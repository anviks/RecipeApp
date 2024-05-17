using System.ComponentModel.DataAnnotations;
using Base.Domain;
using AppResource = App.Resources.App.BLL.DTO;

namespace App.BLL.DTO;

public class ReviewRequest : BaseEntityId
{
    [Range(1, 10)]
    [Display(ResourceType = typeof(AppResource.Review), Name = "Rating")]
    public short Rating { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Review), Name = "Comment")]
    public string Comment { get; set; } = default!;
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "RecipeSingular")]
    public Guid RecipeId { get; set; }
}