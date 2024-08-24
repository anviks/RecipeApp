using System.ComponentModel.DataAnnotations;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Resources.Errors;
using Resource = RecipeApp.Resources.Entities;

namespace RecipeApp.Application.DTO;

public class ReviewRequest : BaseEntityId
{
    [Range(1, 10, ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "ValueBetween")]
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.Review), Name = "Rating")]
    public short Rating { get; set; }
    
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.Review), Name = "Comment")]
    public string Comment { get; set; } = default!;
    
    [Required(ErrorMessageResourceType = typeof(ValidationErrors), ErrorMessageResourceName = "Required")]
    [Display(ResourceType = typeof(Resource.Recipe), Name = "RecipeSingular")]
    public Guid RecipeId { get; set; }
}