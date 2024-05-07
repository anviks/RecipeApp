using BLL_DTO = App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

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
    public BLL_DTO.AppUser AuthorUser { get; set; } = default!;
    public DateTime? UpdatedAt { get; set; }
    public BLL_DTO.AppUser? UpdatingUser { get; set; }
}