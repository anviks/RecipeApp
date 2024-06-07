using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class TicketCreateEditViewModel
{
    public Ticket Ticket { get; set; } = default!;
    public SelectList? Users { get; set; }
    public SelectList? Raffles { get; set; }
}