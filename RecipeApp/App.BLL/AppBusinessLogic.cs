using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Domain.Identity;
using AutoMapper;
using Base.BLL;
using Base.Contracts.BLL;

namespace App.BLL;

public class AppBusinessLogic(IAppUnitOfWork unitOfWork, IMapper mapper)
    : BaseBusinessLogic(unitOfWork), IAppBusinessLogic
{
    private IRecipeService? _recipes;
    public IRecipeService Recipes => _recipes ??= new RecipeService(unitOfWork, unitOfWork.Recipes, mapper);
}