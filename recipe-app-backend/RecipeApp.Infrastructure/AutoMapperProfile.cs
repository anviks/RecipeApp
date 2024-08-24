using AutoMapper;
using Category = RecipeApp.Infrastructure.Data.EntityFramework.Entities.Category;
using Ingredient = RecipeApp.Infrastructure.Data.EntityFramework.Entities.Ingredient;
using IngredientType = RecipeApp.Infrastructure.Data.EntityFramework.Entities.IngredientType;
using IngredientTypeAssociation = RecipeApp.Infrastructure.Data.DTO.IngredientTypeAssociation;
using Recipe = RecipeApp.Infrastructure.Data.EntityFramework.Entities.Recipe;
using RecipeCategory = RecipeApp.Infrastructure.Data.EntityFramework.Entities.RecipeCategory;
using RecipeIngredient = RecipeApp.Infrastructure.Data.EntityFramework.Entities.RecipeIngredient;
using Review = RecipeApp.Infrastructure.Data.EntityFramework.Entities.Review;
using Unit = RecipeApp.Infrastructure.Data.DTO.Unit;

namespace RecipeApp.Infrastructure;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Category, Category>().ReverseMap();
        CreateMap<Ingredient, Ingredient>().ReverseMap();
        CreateMap<IngredientType, IngredientType>().ReverseMap();
        CreateMap<IngredientTypeAssociation, IngredientTypeAssociation>().ReverseMap();
        CreateMap<RecipeCategory, RecipeCategory>().ReverseMap();
        CreateMap<RecipeIngredient, RecipeIngredient>().ReverseMap();
        CreateMap<Recipe, Recipe>().ReverseMap();
        CreateMap<Review, Review>().ReverseMap();
        CreateMap<RecipeApp.Infrastructure.Data.EntityFramework.Entities.Unit, Unit>().ReverseMap();
    }
}