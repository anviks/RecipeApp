using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Base.Infrastructure.Data;

public class BaseRefreshToken : BaseEntityId
{
    [MaxLength(64)] 
    public string RefreshToken { get; set; } = Guid.NewGuid().ToString();
    public DateTime ExpirationDateTime { get; set; } = DateTime.UtcNow.AddDays(7);
    
    [MaxLength(64)] 
    public string? PreviousRefreshToken { get; set; }
    // public DateTime PreviousExpirationDateTime { get; set; } = DateTime.UtcNow.AddDays(7);
    public DateTime? PreviousExpirationDateTime { get; set; }
}