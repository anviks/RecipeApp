using Base.Domain;

namespace App.BLL.DTO;

public class RecipeResponse : BaseEntityId
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageFileName { get; set; } = default!;
    public List<string> Instructions { get; set; } = default!;
    public int PreparationTime { get; set; }
    public int CookingTime { get; set; }
    public int Servings { get; set; }
    public bool IsVegetarian { get; set; }
    public bool IsVegan { get; set; }
    public bool IsGlutenFree { get; set; }
    public DateTime CreatedAt { get; set; }
    public string AuthorUser { get; set; } = default!;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatingUser { get; set; }
}