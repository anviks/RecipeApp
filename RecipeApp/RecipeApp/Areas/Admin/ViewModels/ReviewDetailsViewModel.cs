using App.BLL.DTO;

namespace RecipeApp.Areas.Admin.ViewModels;

public class ReviewDetailsViewModel
{
    public ReviewResponse ReviewResponse { get; set; } = default!;
    public string RecipeTitle { get; set; } = default!;
}