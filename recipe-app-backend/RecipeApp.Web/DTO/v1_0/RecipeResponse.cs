using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Web.DTO.v1_0.Identity;

namespace RecipeApp.Web.DTO.v1_0;

public class RecipeResponse : BaseEntityId
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageFileUrl { get; set; } = default!;
    public List<string> Instructions { get; set; } = default!;
    public int PreparationTime { get; set; }
    public int CookingTime { get; set; }
    public int Servings { get; set; }
    public bool IsVegetarian { get; set; }
    public bool IsVegan { get; set; }
    public bool IsGlutenFree { get; set; }
    public DateTime CreatedAt { get; set; }
    public AppUser AuthorUser { get; set; } = default!;
    public DateTime? UpdatedAt { get; set; }
    public AppUser? UpdatingUser { get; set; }
    public ICollection<Ingredient>? Ingredients { get; set; }
    public ICollection<Category>? Categories { get; set; }
}