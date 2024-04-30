using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;

namespace RecipeApp.ApiControllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController(IAppUnitOfWork unitOfWork) : ControllerBase
{
    // GET: Ingredients
    public async Task<ActionResult<List<Ingredient>>> Index()
    {
        return Ok(await unitOfWork.Ingredients.FindAllAsync());
    }
}