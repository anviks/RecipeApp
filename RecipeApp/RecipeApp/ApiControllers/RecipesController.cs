using System.Net;
using App.Contracts.BLL;
using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using App.DTO.v1_0;
using Asp.Versioning;
using Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLL_DTO = App.BLL.DTO;

namespace RecipeApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class RecipesController(
    IAppBusinessLogic businessLogic,
    UserManager<AppUser> userManager,
    EntityMapper<App.DTO.v1_0.Recipe, BLL_DTO.RecipeResponse> mapper)
    : ControllerBase
{
    // GET: api/v1/Recipes
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<App.DTO.v1_0.Recipe>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<App.DTO.v1_0.Recipe>>> GetRecipes()
    {
        var allRecipes = await businessLogic.Recipes.FindAllAsync();
        return Ok(allRecipes.Select(mapper.Map).ToList());
    }

    // GET: api/v1/Recipes/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<App.DTO.v1_0.Recipe>(StatusCodes.Status200OK)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<App.DTO.v1_0.Recipe>> GetRecipe(Guid id)
    {
        BLL_DTO.RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(id);
        
        if (recipe == null)
        {
            return NotFound(
                new RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Recipe with id {id} not found."
                }
            );
        }

        return Ok(mapper.Map(recipe));
    }

    // PUT: api/v1/Recipes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutRecipe(Guid id, App.DTO.v1_0.Recipe recipe)
    {
        if (id != recipe.Id)
        {
            return BadRequest();
        }

        BLL_DTO.RecipeResponse? existingRecipe = await businessLogic.Recipes.FindAsync(id);
        if (existingRecipe == null)
        {
            return NotFound(
                new RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Recipe with id {id} not found."
                }
            );
        }

        try
        {
            businessLogic.Recipes.Update(mapper.Map(recipe)!);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.Recipes.ExistsAsync(id))
            {
                return NotFound(
                    new RestApiErrorResponse
                    {
                        Status = HttpStatusCode.NotFound,
                        Error = $"Recipe with id {id} not found."
                    }
                );
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/v1/Recipes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType<App.DTO.v1_0.Recipe>(StatusCodes.Status201Created)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<App.DTO.v1_0.Recipe>> PostRecipe(App.DTO.v1_0.Recipe recipe)
    {
        businessLogic.Recipes.Add(mapper.Map(recipe)!);
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetRecipe", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = recipe.Id
        }, recipe);
    }

    // DELETE: api/v1/Recipes/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecipe(Guid id)
    {
        BLL_DTO.RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(id);
        
        if (recipe == null)
        {
            return NotFound();
        }

        await businessLogic.Recipes.RemoveAsync(recipe);
        await businessLogic.SaveChangesAsync();

        return NoContent();
    }
}