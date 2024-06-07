using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class RaffleResultCreateEditViewModel
{
    public RaffleResult RaffleResult { get; set; }
    public SelectList Raffles { get; set; }
    public SelectList Users { get; set; }
}