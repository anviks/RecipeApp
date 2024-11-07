using System.Net;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Contracts;
using RecipeApp.Application.DTO;
using RecipeApp.Base.Helpers;
using v1_0 = RecipeApp.Web.DTO.v1_0;

namespace RecipeApp.Web.ApiControllers;

/// <summary>
/// API controller for managing recipe categories.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class RecipeCategoriesController(
    IAppBusinessLogic businessLogic,
    IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<v1_0.RecipeCategory, RecipeCategory> _mapper = new(mapper);

    /// <summary>
    /// Get all recipe categories.
    /// </summary>
    /// <returns>A list of recipe categories.</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<v1_0.RecipeCategory>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.RecipeCategory>>> GetRecipeCategories()
    {
        var recipeCategories = await businessLogic.RecipeCategories.GetAllAsync();
        return Ok(recipeCategories.Select(_mapper.Map));
    }

    /// <summary>
    /// Get a specific recipe category by id.
    /// </summary>
    /// <param name="id">The id of the recipe category.</param>
    /// <returns>The recipe category with the specified id.</returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.RecipeCategory), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.RecipeCategory>> GetRecipeCategory(Guid id)
    {
        RecipeCategory? recipeCategory = await businessLogic.RecipeCategories.GetByIdAsync(id);

        if (recipeCategory == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"RecipeCategory with id {id} not found."
                });
        }

        return Ok(_mapper.Map(recipeCategory));
    }

    /// <summary>
    /// Update a specific recipe category.
    /// </summary>
    /// <param name="id">The id of the recipe category to update.</param>
    /// <param name="recipeCategory">The updated recipe category data.</param>
    /// <returns>A status indicating the result of the update operation.</returns>
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutRecipeCategory(Guid id, v1_0.RecipeCategory recipeCategory)
    {
        if (id != recipeCategory.Id)
        {
            return BadRequest(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "Id in the request body does not match the id in the URL."
                });
        }

        try
        {
            await businessLogic.RecipeCategories.UpdateAsync(_mapper.Map(recipeCategory)!);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.RecipeCategories.ExistsAsync(id))
            {
                return NotFound(
                    new v1_0.RestApiErrorResponse
                    {
                        Status = HttpStatusCode.NotFound,
                        Error = $"RecipeCategory with id {id} not found."
                    });
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Create a new recipe category.
    /// </summary>
    /// <param name="recipeCategory">The recipe category data.</param>
    /// <returns>The created recipe category.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.RecipeCategory), StatusCodes.Status201Created)]
    public async Task<ActionResult<v1_0.RecipeCategory>> PostRecipeCategory(v1_0.RecipeCategory recipeCategory)
    {
        await businessLogic.RecipeCategories.AddAsync(_mapper.Map(recipeCategory)!);
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetRecipeCategory", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = recipeCategory.Id
        }, recipeCategory);
    }

    /// <summary>
    /// Delete a specific recipe category by id.
    /// </summary>
    /// <param name="id">The id of the recipe category to delete.</param>
    /// <returns>A status indicating the result of the delete operation.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecipeCategory(Guid id)
    {
        RecipeCategory? recipeCategory = await businessLogic.RecipeCategories.GetByIdAsync(id);
        if (recipeCategory == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"RecipeCategory with id {id} not found."
                });
        }

        await businessLogic.RecipeCategories.DeleteAsync(recipeCategory);
        await businessLogic.SaveChangesAsync();

        return NoContent();
    }
}