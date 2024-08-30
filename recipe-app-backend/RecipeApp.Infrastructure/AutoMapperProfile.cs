using AutoMapper;

namespace RecipeApp.Infrastructure;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Data.EntityFramework.Entities.Category, Data.DTO.Category>().ReverseMap();
        CreateMap<Data.EntityFramework.Entities.Ingredient, Data.DTO.Ingredient>().ReverseMap();
        CreateMap<Data.EntityFramework.Entities.IngredientType, Data.DTO.IngredientType>().ReverseMap();
        CreateMap<Data.EntityFramework.Entities.IngredientTypeAssociation, Data.DTO.IngredientTypeAssociation>().ReverseMap();
        CreateMap<Data.EntityFramework.Entities.RecipeCategory, Data.DTO.RecipeCategory>().ReverseMap();
        CreateMap<Data.EntityFramework.Entities.RecipeIngredient, Data.DTO.RecipeIngredient>().ReverseMap();
        CreateMap<Data.EntityFramework.Entities.Recipe, Data.DTO.Recipe>().ReverseMap();
        CreateMap<Data.EntityFramework.Entities.Review, Data.DTO.Review>().ReverseMap();
        CreateMap<Data.EntityFramework.Entities.Unit, Data.DTO.Unit>().ReverseMap();
    }
}