using Base.Domain;

namespace App.DAL.DTO;

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
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatingUserId { get; set; }
}