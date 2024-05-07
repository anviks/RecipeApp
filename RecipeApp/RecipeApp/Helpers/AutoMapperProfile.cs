using AutoMapper;

namespace RecipeApp.Helpers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.BLL.DTO.RecipeResponse, App.DTO.v1_0.RecipeResponse>().ReverseMap();
        CreateMap<App.BLL.DTO.RecipeRequest, App.DTO.v1_0.RecipeRequest>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppUser, App.DTO.v1_0.Identity.AppUser>();
    }
}
