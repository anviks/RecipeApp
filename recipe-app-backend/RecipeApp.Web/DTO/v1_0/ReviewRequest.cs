using System.ComponentModel.DataAnnotations;
using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Web.DTO.v1_0;

public class ReviewRequest : BaseEntityId
{
    [Range(1, 10)]
    public short Rating { get; set; }
    public string Comment { get; set; } = default!;
    public Guid RecipeId { get; set; }
}