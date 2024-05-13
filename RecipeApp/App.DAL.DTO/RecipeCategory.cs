using Base.Domain;

namespace App.DAL.DTO;

public class RecipeCategory : BaseEntityId
{
    public Guid CategoryId { get; set; }
    public Guid RecipeId { get; set; }
}