using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Review : BaseEntityId
{
    // TODO: add in ERD schema
    public bool Edited { get; set; }
    public short Rating { get; set; }
    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    
    public Guid RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
}