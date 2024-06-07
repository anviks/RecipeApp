using App.DAL.DTO;

namespace WebApp.ViewModels;

public class ActivityDetailsDeleteViewModel
{
    public Activity Activity { get; set; }
    public string ActivityType { get; set; }
    public string User { get; set; }
}