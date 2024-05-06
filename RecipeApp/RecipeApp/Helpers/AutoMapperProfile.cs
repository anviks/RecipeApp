using AutoMapper;

namespace RecipeApp.Helpers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.BLL.DTO.RecipeResponse, App.DTO.v1_0.Recipe>().ReverseMap();
    }
}
