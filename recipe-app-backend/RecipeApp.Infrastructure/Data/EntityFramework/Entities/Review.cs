using System.ComponentModel.DataAnnotations;
using RecipeApp.Base.Infrastructure.Data;
using RecipeApp.Infrastructure.Data.EntityFramework.Entities.Identity;

namespace RecipeApp.Infrastructure.Data.EntityFramework.Entities;

public class Review : BaseEntityId
{
    public bool Edited { get; set; }
    [Range(1, 10)]
    public short Rating { get; set; }
    
    [MaxLength(1024)]
    public string Comment { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    
    public Guid RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
}