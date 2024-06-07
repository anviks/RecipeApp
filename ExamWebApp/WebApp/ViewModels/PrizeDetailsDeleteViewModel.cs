using App.DAL.DTO;

namespace WebApp.ViewModels;

public class PrizeDetailsDeleteViewModel
{
    public Prize Prize { get; set; }
    public string Raffle { get; set; }
    public string RaffleResult { get; set; }
}