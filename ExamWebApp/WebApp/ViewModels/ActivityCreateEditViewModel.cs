using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class ActivityCreateEditViewModel
{
    public Activity Activity { get; set; } = default!;
    public SelectList? ActivityTypes { get; set; }
    public SelectList? Users { get; set; }
}