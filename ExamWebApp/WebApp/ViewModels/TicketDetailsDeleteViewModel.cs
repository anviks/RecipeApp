using App.DAL.DTO;

namespace WebApp.ViewModels;

public class TicketDetailsDeleteViewModel
{
    public Ticket Ticket { get; set; } = default!;
    public string User { get; set; } = default!;
    public string Raffle { get; set; } = default!;
}