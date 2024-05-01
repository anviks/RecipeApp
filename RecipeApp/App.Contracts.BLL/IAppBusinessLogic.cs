using App.Contracts.BLL.Services;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBusinessLogic : IBusinessLogic
{
    IRecipeService Recipes { get; }
}