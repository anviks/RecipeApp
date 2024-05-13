using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeApp.Areas.Admin.ViewModels;

public class IngredientTypeAssociationCreateEditViewModel
{
    public IngredientTypeAssociation IngredientTypeAssociation { get; set; } = default!;
    public SelectList? IngredientSelectList { get; set; }
    public SelectList? IngredientTypeSelectList { get; set; }
}