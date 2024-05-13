using AutoMapper;
using v1_0 = App.DTO.v1_0;
using BLL_DTO = App.BLL.DTO;

namespace RecipeApp.Helpers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<BLL_DTO.Identity.AppUser, v1_0.Identity.AppUser>();
        
        CreateMap<BLL_DTO.Category, v1_0.Category>().ReverseMap();
        
        CreateMap<BLL_DTO.Ingredient, v1_0.Ingredient>().ReverseMap();
        
        CreateMap<BLL_DTO.IngredientType, v1_0.IngredientType>().ReverseMap();
        
        CreateMap<BLL_DTO.IngredientTypeAssociation, v1_0.IngredientTypeAssociation>().ReverseMap();
        
        CreateMap<BLL_DTO.RecipeCategory, v1_0.RecipeCategory>().ReverseMap();
        
        CreateMap<BLL_DTO.RecipeIngredient, v1_0.RecipeIngredient>().ReverseMap();
        
        CreateMap<BLL_DTO.RecipeResponse, v1_0.RecipeResponse>().ReverseMap();
        CreateMap<BLL_DTO.RecipeRequest, v1_0.RecipeRequest>().ReverseMap();
        
        CreateMap<BLL_DTO.Review, v1_0.Review>().ReverseMap();
        
        CreateMap<BLL_DTO.Unit, v1_0.Unit>().ReverseMap();
    }
}
