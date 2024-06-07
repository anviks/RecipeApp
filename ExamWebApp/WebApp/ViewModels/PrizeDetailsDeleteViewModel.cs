using App.DAL.DTO;

namespace WebApp.ViewModels;

public class PrizeDetailsDeleteViewModel
{
    public Prize Prize { get; set; } = default!;
    public string Raffle { get; set; } = default!;
    public string RaffleResult { get; set; } = default!;
}