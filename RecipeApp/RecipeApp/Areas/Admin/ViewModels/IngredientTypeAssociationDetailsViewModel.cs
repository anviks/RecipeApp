using App.BLL.DTO;

namespace RecipeApp.Areas.Admin.ViewModels;

public class IngredientTypeAssociationDetailsViewModel
{
    public IngredientTypeAssociation IngredientTypeAssociation { get; set; } = default!;
    public string IngredientName { get; set; } = default!;
    public string IngredientTypeName { get; set; } = default!;
}