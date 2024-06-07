using App.DAL.DTO;

namespace WebApp.ViewModels;

public class TicketDetailsDeleteViewModel
{
    public Ticket Ticket { get; set; }
    public string User { get; set; }
    public string Raffle { get; set; }
}