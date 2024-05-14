using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class ReviewResponse : BaseEntityId
{
    public bool Edited { get; set; }
    [Range(1, 10)]
    public short Rating { get; set; }
    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public Guid RecipeId { get; set; }
    public AppUser User { get; set; } = default!;
}