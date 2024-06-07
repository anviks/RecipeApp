using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class PrizeCreateEditViewModel
{
    public Prize Prize { get; set; } = default!;
    public SelectList? Raffles { get; set; }
    public SelectList? RaffleResults { get; set; }
}