using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Data.EntityFramework.Entities.Identity;

namespace RecipeApp.Infrastructure.Data.DTO;

public class Recipe : BaseEntityId
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
    public Guid AuthorUserId { get; set; }
    public AppUser AuthorUser { get; set; } = default!;
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatingUserId { get; set; }
    public AppUser? UpdatingUser { get; set; }
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    public ICollection<RecipeCategory>? RecipeCategories { get; set; }
}