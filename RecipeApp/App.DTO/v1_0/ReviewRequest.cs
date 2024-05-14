using Base.Domain;

namespace App.DTO.v1_0;

public class ReviewRequest : BaseEntityId
{
    public short Rating { get; set; }
    public string Comment { get; set; } = default!;
    public Guid RecipeId { get; set; }
}