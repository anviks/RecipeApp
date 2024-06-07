using App.DAL.DTO;

namespace WebApp.ViewModels;

public class RaffleResultDetailsDeleteViewModel
{
    public RaffleResult RaffleResult { get; set; } = default!;
    public string Raffle { get; set; } = default!;
    public string User { get; set; } = default!;
}