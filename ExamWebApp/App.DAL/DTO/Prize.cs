using Base.Domain;

namespace App.DAL.DTO;

public class Prize : BaseEntityId
{
    public string PrizeName { get; set; } = default!;
    public Guid? RaffleResultId { get; set; }
    public Guid RaffleId { get; set; }
}