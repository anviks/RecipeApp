using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;
using Helpers;
using DAL_DTO = App.DAL.DTO;
using BLL_DTO = App.BLL.DTO;

namespace App.BLL.Services;

public class RecipeIngredientService(
    IUnitOfWork unitOfWork,
    IRecipeIngredientRepository repository,
    IMapper mapper)
    : BaseEntityService<DAL_DTO.RecipeIngredient, BLL_DTO.RecipeIngredient, IRecipeIngredientRepository>(unitOfWork, repository,
            new EntityMapper<DAL_DTO.RecipeIngredient, BLL_DTO.RecipeIngredient>(mapper)),
        IRecipeIngredientService;