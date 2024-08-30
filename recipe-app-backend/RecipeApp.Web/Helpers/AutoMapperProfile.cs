using AutoMapper;
using RecipeApp.Application.DTO;
using AppUser = RecipeApp.Application.DTO.Identity.AppUser;
using v1_0 = RecipeApp.Web.DTO.v1_0;

namespace RecipeApp.Web.Helpers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppUser, v1_0.Identity.AppUser>();
        
        CreateMap<Category, v1_0.Category>().ReverseMap();
        
        CreateMap<Ingredient, v1_0.Ingredient>().ReverseMap();
        
        CreateMap<IngredientType, v1_0.IngredientType>().ReverseMap();
        
        CreateMap<IngredientTypeAssociation, v1_0.IngredientTypeAssociation>().ReverseMap();
        
        CreateMap<RecipeCategory, v1_0.RecipeCategory>().ReverseMap();
        
        CreateMap<RecipeIngredient, v1_0.RecipeIngredient>().ReverseMap();
        
        CreateMap<RecipeResponse, v1_0.RecipeResponse>().ReverseMap();
        CreateMap<RecipeRequest, v1_0.RecipeRequest>().ReverseMap();
        
        CreateMap<ReviewResponse, v1_0.ReviewResponse>().ReverseMap();
        CreateMap<ReviewRequest, v1_0.ReviewRequest>().ReverseMap();
        
        CreateMap<Unit, v1_0.Unit>().ReverseMap();
    }
}
