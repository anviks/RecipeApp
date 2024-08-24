using AutoMapper;
using DAL = RecipeApp.Infrastructure.Data.DTO;

namespace RecipeApp.Application;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Infrastructure.Data.EntityFramework.Entities.Identity.AppUser, DTO.Identity.AppUser>().ReverseMap();
        
        CreateMap<DAL.Category, DTO.Category>().ReverseMap();
        
        CreateMap<DAL.Ingredient, DTO.Ingredient>().ReverseMap();

        CreateMap<DAL.IngredientType, DTO.IngredientType>().ReverseMap();
        
        CreateMap<DAL.IngredientTypeAssociation, DTO.IngredientTypeAssociation>().ReverseMap();
        
        CreateMap<DAL.RecipeCategory, DTO.RecipeCategory>().ReverseMap();
        
        CreateMap<DAL.RecipeIngredient, DTO.RecipeIngredient>().ReverseMap();
        
        CreateMap<DAL.Recipe, DTO.RecipeResponse>().ReverseMap();
        CreateMap<DAL.Recipe, DTO.RecipeRequest>().ReverseMap();
        CreateMap<DTO.RecipeRequest, DTO.RecipeResponse>().ReverseMap();
        
        CreateMap<DAL.Review, DTO.ReviewResponse>().ReverseMap();
        CreateMap<DAL.Review, DTO.ReviewRequest>().ReverseMap();
        CreateMap<DTO.ReviewRequest, DTO.ReviewResponse>().ReverseMap();
        
        CreateMap<DAL.Unit, DTO.Unit>().ReverseMap();
    }
}