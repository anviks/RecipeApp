using System.ComponentModel.DataAnnotations;
using Base.Domain;
using AppResource = App.Resources.App.BLL.DTO;

namespace App.BLL.DTO;

public class RecipeCategory : BaseEntityId
{
    [Display(ResourceType = typeof(AppResource.Category), Name = "CategorySingular")]
    public Guid CategoryId { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "RecipeSingular")]
    public Guid RecipeId { get; set; }
}