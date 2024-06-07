using Base.Domain;

namespace App.DAL.DTO;

public class Raffle : BaseEntityId
{
    public string RaffleName { get; set; } = default!;
    public bool VisibleToPublic { get; set; }
    public bool AllowAnonymousUsers { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid CompanyId { get; set; }
}