using Base.Domain;

namespace App.DTO.v1_0;

public class Prize : BaseEntityId
{
    public string PrizeName { get; set; } = default!;
    public Guid? RaffleResultId { get; set; }
    public Guid RaffleId { get; set; }
}