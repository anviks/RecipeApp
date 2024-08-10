using AutoMapper;
using AppDomain = App.Domain;
using DAL_DTO = App.DAL.DTO;

namespace App.DAL.EF;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppDomain.Category, DAL_DTO.Category>().ReverseMap();
        CreateMap<AppDomain.Ingredient, DAL_DTO.Ingredient>().ReverseMap();
        CreateMap<AppDomain.IngredientType, DAL_DTO.IngredientType>().ReverseMap();
        CreateMap<AppDomain.IngredientTypeAssociation, DAL_DTO.IngredientTypeAssociation>().ReverseMap();
        CreateMap<AppDomain.RecipeCategory, DAL_DTO.RecipeCategory>().ReverseMap();
        CreateMap<AppDomain.RecipeIngredient, DAL_DTO.RecipeIngredient>().ReverseMap();
        CreateMap<AppDomain.Recipe, DAL_DTO.Recipe>().ReverseMap();
        CreateMap<AppDomain.Review, DAL_DTO.Review>().ReverseMap();
        CreateMap<AppDomain.Unit, DAL_DTO.Unit>().ReverseMap();
    }
}