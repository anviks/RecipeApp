using RecipeApp.Application.DTO;

namespace RecipeApp.Web.ViewModels;

public class RecipeCreateEditViewModel
{
    public RecipeRequest RecipeRequest { get; set; } = default!;
    // public int InstructionCount { get; set; }
}