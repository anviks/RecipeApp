using App.DTO.v1_0.Identity;
using Base.Domain;

namespace App.DTO.v1_0;

public class ReviewResponse : BaseEntityId
{
    public bool Edited { get; set; }
    public short Rating { get; set; }
    public string Comment { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public Guid RecipeId { get; set; }
    public AppUser User { get; set; } = default!;
}