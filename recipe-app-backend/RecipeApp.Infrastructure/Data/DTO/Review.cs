using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Data.EntityFramework.Entities.Identity;

namespace RecipeApp.Infrastructure.Data.DTO;

public class Review : BaseEntityId
{
    public bool Edited { get; set; }
    public short Rating { get; set; }
    public string Comment { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public Guid RecipeId { get; set; }
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = default!;
}