using System.Net;
using App.BLL.Exceptions;
using App.Contracts.BLL;
using App.Domain.Identity;
using Asp.Versioning;
using AutoMapper;
using Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using v1_0 = App.DTO.v1_0;
using BLL_DTO = App.BLL.DTO;

namespace RecipeApp.ApiControllers;

/// <summary>
/// API controller for managing recipes.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class RecipesController(
    IAppBusinessLogic businessLogic,
    UserManager<AppUser> userManager,
    IMapper mapper,
    IWebHostEnvironment environment)
    : ControllerBase
{
    private readonly EntityMapper<v1_0.RecipeResponse, BLL_DTO.RecipeResponse> _responseMapper = new(mapper);
    private readonly EntityMapper<v1_0.RecipeRequest, BLL_DTO.RecipeRequest> _requestMapper = new(mapper);

    /// <summary>
    /// Get all recipes.
    /// </summary>
    /// <returns>A list of recipes.</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<v1_0.RecipeResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.RecipeResponse>>> GetRecipes()
    {
        var allRecipes = await businessLogic.Recipes.FindAllAsync();
        return Ok(allRecipes.Select(_responseMapper.Map).ToList());
    }

    /// <summary>
    /// Get a specific recipe by id.
    /// </summary>
    /// <param name="id">The id of the recipe.</param>
    /// <returns>The recipe with the specified id.</returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.RecipeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.RecipeResponse>> GetRecipe(Guid id)
    {
        BLL_DTO.RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(id);

        if (recipe == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Recipe with id {id} not found."
                });
        }

        return Ok(_responseMapper.Map(recipe));
    }

    /// <summary>
    /// Update a specific recipe.
    /// </summary>
    /// <param name="id">The id of the recipe to update.</param>
    /// <param name="request">The updated recipe data.</param>
    /// <returns>A status indicating the result of the update operation.</returns>
    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutRecipe(Guid id, v1_0.RecipeRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "Id in the request body does not match the id in the URL."
                });
        }

        BLL_DTO.RecipeResponse? existingRecipe = await businessLogic.Recipes.FindAsync(id);
        if (existingRecipe == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Recipe with id {id} not found."
                });
        }

        try
        {
            await businessLogic.Recipes.UpdateAsync(_requestMapper.Map(request)!,
                Guid.Parse(userManager.GetUserId(User)!), environment.WebRootPath);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.Recipes.ExistsAsync(id))
            {
                return NotFound(
                    new v1_0.RestApiErrorResponse
                    {
                        Status = HttpStatusCode.NotFound,
                        Error = $"Recipe with id {id} not found."
                    });
            }

            throw;
        }

        return NoContent();
    }

    /// <summary>
    /// Create a new recipe.
    /// </summary>
    /// <param name="request">The recipe data.</param>
    /// <returns>The created recipe.</returns>
    [HttpPost]
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.RecipeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<v1_0.RecipeResponse>> PostRecipe(
        [FromForm] v1_0.RecipeRequest request)
    {
        BLL_DTO.RecipeResponse savedRecipe;
        try
        {
            savedRecipe = await businessLogic.Recipes.AddAsync(
                _requestMapper.Map(request)!,
                Guid.Parse(userManager.GetUserId(User)!),
                environment.WebRootPath);
        }
        catch (MissingImageException)
        {
            return BadRequest(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "Image file is required."
                });
        }

        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetRecipe", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = savedRecipe.Id
        }, savedRecipe);
    }

    /// <summary>
    /// Delete a specific recipe by id.
    /// </summary>
    /// <param name="id">The id of the recipe to delete.</param>
    /// <returns>A status indicating the result of the delete operation.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecipe(Guid id)
    {
        BLL_DTO.RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(id);

        if (recipe == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Recipe with id {id} not found."
                });
        }

        await businessLogic.Recipes.RemoveAsync(recipe);
        await businessLogic.SaveChangesAsync();

        return NoContent();
    }
}
