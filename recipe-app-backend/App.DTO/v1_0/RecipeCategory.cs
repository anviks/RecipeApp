using Base.Domain;

namespace App.DTO.v1_0;

public class RecipeCategory : BaseEntityId
{
    public Guid CategoryId { get; set; }
    public Guid RecipeId { get; set; }
}