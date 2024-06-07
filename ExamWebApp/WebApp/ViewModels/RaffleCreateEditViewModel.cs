using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class RaffleCreateEditViewModel
{
    public Raffle Raffle { get; set; } = default!;
    public SelectList? Companies { get; set; }
}