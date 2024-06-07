using App.DAL.DTO;

namespace WebApp.ViewModels;

public class RaffleDetailsDeleteViewModel
{
    public Raffle Raffle { get; set; } = default!;
    public string Company { get; set; } = default!;
}