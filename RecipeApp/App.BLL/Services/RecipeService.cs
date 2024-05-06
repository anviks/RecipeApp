using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using DAL_DTO = App.DAL.DTO;
using BLL_DTO = App.BLL.DTO;
using App.DAL.EF.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.BLL;
using Base.Contracts.DAL;
using Helpers;
using Microsoft.AspNetCore.Http;

namespace App.BLL.Services;

public class RecipeService(
    IUnitOfWork unitOfWork,
    IRecipeRepository repository,
    IMapper mapper)
    : BaseEntityService<DAL_DTO.Recipe, BLL_DTO.RecipeResponse, IRecipeRepository>(unitOfWork, repository, new EntityMapper<DAL_DTO.Recipe, BLL_DTO.RecipeResponse>(mapper)),
        IRecipeService
{
    private readonly EntityMapper<BLL_DTO.RecipeRequest, BLL_DTO.RecipeResponse> _recipeMapper = new(mapper);
    
    public BLL_DTO.RecipeResponse Add(BLL_DTO.RecipeResponse recipeResponse, Guid userId)
    {
        DAL_DTO.Recipe dalRecipe = Mapper.Map(recipeResponse)!;
        dalRecipe.CreatedAt = DateTime.Now.ToUniversalTime();
        dalRecipe.AuthorUserId = userId;
        DAL_DTO.Recipe addedRecipe = Repository.Add(dalRecipe);
        return Mapper.Map(addedRecipe)!;
    }

    public BLL_DTO.RecipeResponse Add(BLL_DTO.RecipeRequest recipeRequest, Guid userId)
    {
        BLL_DTO.RecipeResponse recipeResponse = _recipeMapper.Map(recipeRequest)!;
        DAL_DTO.Recipe dalRecipe = Mapper.Map(recipeResponse)!;
        dalRecipe.CreatedAt = DateTime.Now.ToUniversalTime();
        dalRecipe.AuthorUserId = userId;
        DAL_DTO.Recipe addedRecipe = Repository.Add(dalRecipe);
        return Mapper.Map(addedRecipe)!;
    }

    public BLL_DTO.RecipeResponse Update(BLL_DTO.RecipeResponse recipeResponse, Guid userId)
    {
        DAL_DTO.Recipe dalRecipe = Mapper.Map(recipeResponse)!;
        dalRecipe.UpdatedAt = DateTime.Now.ToUniversalTime();
        dalRecipe.UpdatingUserId = userId;
        DAL_DTO.Recipe updatedRecipe = Repository.Update(dalRecipe);
        return Mapper.Map(updatedRecipe)!;
    }
}