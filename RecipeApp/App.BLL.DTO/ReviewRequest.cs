using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Base.Resources;
using AppResource = App.Resources.App.BLL.DTO;

namespace App.BLL.DTO;

public class ReviewRequest : BaseEntityId
{
    [Range(1, 10, ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "ValueBetween")]
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(AppResource.Review), Name = "Rating")]
    public short Rating { get; set; }
    
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(AppResource.Review), Name = "Comment")]
    public string Comment { get; set; } = default!;
    
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "RecipeSingular")]
    public Guid RecipeId { get; set; }
}