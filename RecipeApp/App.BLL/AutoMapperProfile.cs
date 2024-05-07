using AutoMapper;
using DAL_DTO = App.DAL.DTO;
using BLL_DTO = App.BLL.DTO;

namespace App.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<DAL_DTO.Recipe, BLL_DTO.RecipeResponse>().ReverseMap();
        CreateMap<DAL_DTO.Recipe, BLL_DTO.RecipeRequest>().ReverseMap();
        CreateMap<BLL_DTO.RecipeRequest, BLL_DTO.RecipeResponse>().ReverseMap();
    }
}