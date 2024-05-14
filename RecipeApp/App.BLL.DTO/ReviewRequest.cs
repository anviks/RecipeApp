using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.BLL.DTO;

public class ReviewRequest : BaseEntityId
{
    [Range(1, 10)]
    public short Rating { get; set; }
    public string Comment { get; set; } = default!;
    public Guid RecipeId { get; set; }
}