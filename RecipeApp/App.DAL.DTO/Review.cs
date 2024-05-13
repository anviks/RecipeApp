using Base.Domain;

namespace App.DAL.DTO;

public class Review : BaseEntityId
{
    public bool Edited { get; set; }
    public short Rating { get; set; }
    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public Guid RecipeId { get; set; }
}