using System.ComponentModel.DataAnnotations;
using Base.Domain;
using AppResource = App.Resources.App.BLL.DTO;

namespace App.BLL.DTO;

public class RecipeIngredient : BaseEntityId
{
    [Display(ResourceType = typeof(AppResource.RecipeIngredient), Name = "CustomUnit")]
    public string? CustomUnit { get; set; }
    
    [Display(ResourceType = typeof(AppResource.RecipeIngredient), Name = "Quantity")]
    public float Quantity { get; set; }
    
    [Display(ResourceType = typeof(AppResource.RecipeIngredient), Name = "IngredientModifier")]
    public string? IngredientModifier { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Unit), Name = "UnitSingular")]
    public Guid? UnitId { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Recipe), Name = "RecipeSingular")]
    public Guid RecipeId { get; set; }
    
    [Display(ResourceType = typeof(AppResource.Ingredient), Name = "IngredientSingular")]
    public Guid IngredientId { get; set; }
}