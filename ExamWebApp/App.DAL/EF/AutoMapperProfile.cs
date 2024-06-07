using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Domain.Activity, App.DAL.DTO.Activity>().ReverseMap();
        CreateMap<Domain.ActivityType, App.DAL.DTO.ActivityType>().ReverseMap();
        CreateMap<Domain.Company, App.DAL.DTO.Company>().ReverseMap();
        CreateMap<Domain.Prize, App.DAL.DTO.Prize>().ReverseMap();
        CreateMap<Domain.Raffle, App.DAL.DTO.Raffle>().ReverseMap();
        CreateMap<Domain.RaffleResult, App.DAL.DTO.RaffleResult>().ReverseMap();
        CreateMap<Domain.Ticket, App.DAL.DTO.Ticket>().ReverseMap();
    }
}