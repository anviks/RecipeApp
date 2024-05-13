using AutoMapper;
using DAL_DTO = App.DAL.DTO;
using BLL_DTO = App.BLL.DTO;

namespace App.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.Domain.Identity.AppUser, BLL_DTO.Identity.AppUser>();
        
        CreateMap<DAL_DTO.Category, BLL_DTO.Category>().ReverseMap();
        
        CreateMap<DAL_DTO.Ingredient, BLL_DTO.Ingredient>().ReverseMap();

        CreateMap<DAL_DTO.IngredientType, BLL_DTO.IngredientType>().ReverseMap();
        
        CreateMap<DAL_DTO.IngredientTypeAssociation, BLL_DTO.IngredientTypeAssociation>().ReverseMap();
        
        CreateMap<DAL_DTO.RecipeCategory, BLL_DTO.RecipeCategory>().ReverseMap();
        
        CreateMap<DAL_DTO.RecipeIngredient, BLL_DTO.RecipeIngredient>().ReverseMap();
        
        CreateMap<DAL_DTO.Recipe, BLL_DTO.RecipeResponse>().ReverseMap();
        CreateMap<DAL_DTO.Recipe, BLL_DTO.RecipeRequest>().ReverseMap();
        CreateMap<BLL_DTO.RecipeRequest, BLL_DTO.RecipeResponse>().ReverseMap();
        
        CreateMap<DAL_DTO.Review, BLL_DTO.Review>().ReverseMap();
        
        CreateMap<DAL_DTO.Unit, BLL_DTO.Unit>().ReverseMap();
    }
}