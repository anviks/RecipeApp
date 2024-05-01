using AutoMapper;
using DAL_DTO = App.DAL.DTO;
using BLL_DTO = App.BLL.DTO;

namespace App.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<DAL_DTO.Recipe, BLL_DTO.Recipe>().ReverseMap();
    }
}