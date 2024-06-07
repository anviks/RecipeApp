using AutoMapper;
using v1_0 = App.DTO.v1_0;

namespace WebApp.Helpers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.Domain.Identity.AppUser, v1_0.Identity.AppUser>();
        
        CreateMap<App.DAL.DTO.Activity, v1_0.Activity>().ReverseMap();
        CreateMap<App.DAL.DTO.ActivityType, v1_0.ActivityType>().ReverseMap();
        CreateMap<App.DAL.DTO.Company, v1_0.Company>().ReverseMap();
        CreateMap<App.DAL.DTO.Prize, v1_0.Prize>().ReverseMap();
        CreateMap<App.DAL.DTO.Raffle, v1_0.Raffle>().ReverseMap();
        CreateMap<App.DAL.DTO.RaffleResult, v1_0.RaffleResult>().ReverseMap();
        CreateMap<App.DAL.DTO.Ticket, v1_0.Ticket>().ReverseMap();
    }
}
