using App.Domain.Identity;
using Base.Domain;

namespace App.DAL.DTO;

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