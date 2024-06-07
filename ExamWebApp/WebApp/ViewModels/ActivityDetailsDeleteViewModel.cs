using App.DAL.DTO;

namespace WebApp.ViewModels;

public class ActivityDetailsDeleteViewModel
{
    public Activity Activity { get; set; } = default!;
    public string ActivityType { get; set; } = default!;
    public string User { get; set; } = default!;
}