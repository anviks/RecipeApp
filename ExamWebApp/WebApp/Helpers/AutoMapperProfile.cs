using AutoMapper;
using v1_0 = App.DTO.v1_0;

namespace WebApp.Helpers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.Domain.Identity.AppUser, v1_0.Identity.AppUser>();
        
        CreateMap<App.DAL.DTO.Sample, v1_0.Sample>().ReverseMap();
    }
}
