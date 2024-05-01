using AutoMapper;
using AppDomain = App.Domain;
using DAL_DTO = App.DAL.DTO;

namespace App.DAL.EF;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppDomain.Recipe, DAL_DTO.Recipe>().ReverseMap();
    }
}