using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using DAL_DTO = App.DAL.DTO;
using BLL_DTO = App.BLL.DTO;
using App.DAL.EF.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Services;

public class RecipeService(
    IUnitOfWork unitOfWork,
    IRecipeRepository repository,
    IMapper mapper)
    : BaseEntityService<DAL_DTO.Recipe, BLL_DTO.Recipe, IRecipeRepository>(unitOfWork, repository, new BllDalMapper<DAL_DTO.Recipe, BLL_DTO.Recipe>(mapper)),
        IRecipeService
{
}