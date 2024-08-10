using App.BLL.DTO;

namespace RecipeApp.ViewModels;

public class RecipeCreateEditViewModel
{
    public RecipeRequest RecipeRequest { get; set; } = default!;
    // public int InstructionCount { get; set; }
}