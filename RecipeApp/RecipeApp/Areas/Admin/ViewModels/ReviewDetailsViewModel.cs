using App.BLL.DTO;

namespace RecipeApp.Areas.Admin.ViewModels;

public class ReviewDetailsViewModel
{
    public Review Review { get; set; } = default!;
    public string RecipeTitle { get; set; } = default!;
    public string UserName { get; set; } = default!;
}