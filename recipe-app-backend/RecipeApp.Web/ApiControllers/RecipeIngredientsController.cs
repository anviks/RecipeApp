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
/// API controller for managing recipe ingredients.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class RecipeIngredientsController(
    IAppBusinessLogic businessLogic,
    IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<v1_0.RecipeIngredient, RecipeIngredient> _mapper = new(mapper);

    /// <summary>
    /// Get all recipe ingredients.
    /// </summary>
    /// <returns>A list of recipe ingredients.</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<v1_0.RecipeIngredient>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.RecipeIngredient>>> GetRecipeIngredients()
    {
        var recipeIngredients = await businessLogic.RecipeIngredients.FindAllAsync();
        return Ok(recipeIngredients.Select(_mapper.Map));
    }

    /// <summary>
    /// Get a specific recipe ingredient by id.
    /// </summary>
    /// <param name="id">The id of the recipe ingredient.</param>
    /// <returns>The recipe ingredient with the specified id.</returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.RecipeIngredient), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.RecipeIngredient>> GetRecipeIngredient(Guid id)
    {
        RecipeIngredient? recipeIngredient = await businessLogic.RecipeIngredients.FindAsync(id);

        if (recipeIngredient == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"RecipeIngredient with id {id} not found."
                });
        }

        return Ok(_mapper.Map(recipeIngredient));
    }

    /// <summary>
    /// Update a specific recipe ingredient.
    /// </summary>
    /// <param name="id">The id of the recipe ingredient to update.</param>
    /// <param name="recipeIngredient">The updated recipe ingredient data.</param>
    /// <returns>A status indicating the result of the update operation.</returns>
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutRecipeIngredient(Guid id, v1_0.RecipeIngredient recipeIngredient)
    {
        if (id != recipeIngredient.Id)
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
            businessLogic.RecipeIngredients.Update(_mapper.Map(recipeIngredient)!);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.RecipeIngredients.ExistsAsync(id))
            {
                return NotFound(
                    new v1_0.RestApiErrorResponse
                    {
                        Status = HttpStatusCode.NotFound,
                        Error = $"RecipeIngredient with id {id} not found."
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
    /// Create a new recipe ingredient.
    /// </summary>
    /// <param name="recipeIngredient">The recipe ingredient data.</param>
    /// <returns>The created recipe ingredient.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.RecipeIngredient), StatusCodes.Status201Created)]
    public async Task<ActionResult<v1_0.RecipeIngredient>> PostRecipeIngredient(v1_0.RecipeIngredient recipeIngredient)
    {
        businessLogic.RecipeIngredients.Add(_mapper.Map(recipeIngredient)!);
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetRecipeIngredient", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = recipeIngredient.Id
        }, recipeIngredient);
    }

    /// <summary>
    /// Delete a specific recipe ingredient by id.
    /// </summary>
    /// <param name="id">The id of the recipe ingredient to delete.</param>
    /// <returns>A status indicating the result of the delete operation.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecipeIngredient(Guid id)
    {
        RecipeIngredient? recipeIngredient = await businessLogic.RecipeIngredients.FindAsync(id);
        if (recipeIngredient == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"RecipeIngredient with id {id} not found."
                });
        }

        await businessLogic.RecipeIngredients.RemoveAsync(recipeIngredient);
        await businessLogic.SaveChangesAsync();

        return NoContent();
    }
}
