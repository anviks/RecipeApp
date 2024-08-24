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
/// API controller for managing ingredients.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
[ApiController]
public class IngredientsController(
    IAppBusinessLogic businessLogic,
    IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<v1_0.Ingredient, Ingredient> _mapper = new(mapper);

    /// <summary>
    /// Get all ingredients.
    /// </summary>
    /// <returns>A list of ingredients.</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<v1_0.Ingredient>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.Ingredient>>> GetIngredients()
    {
        var ingredients = await businessLogic.Ingredients.FindAllAsync();
        return Ok(ingredients.Select(_mapper.Map));
    }
    
    /// <summary>
    /// Get a specific ingredient by id.
    /// </summary>
    /// <param name="id">The id of the ingredient.</param>
    /// <returns>The ingredient with the specified id.</returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.Ingredient), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.Ingredient>> GetIngredient(Guid id)
    {
        Ingredient? ingredient = await businessLogic.Ingredients.FindAsync(id);

        if (ingredient == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Ingredient with id {id} not found."
                });
        }

        return Ok(_mapper.Map(ingredient));
    }
    
    /// <summary>
    /// Update a specific ingredient.
    /// </summary>
    /// <param name="id">The id of the ingredient to update.</param>
    /// <param name="ingredient">The updated ingredient data.</param>
    /// <returns>A status indicating the result of the update operation.</returns>
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutIngredient(Guid id, v1_0.Ingredient ingredient)
    {
        if (id != ingredient.Id)
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
            businessLogic.Ingredients.Update(_mapper.Map(ingredient)!);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.Ingredients.ExistsAsync(id))
            {
                return NotFound(
                    new v1_0.RestApiErrorResponse
                    {
                        Status = HttpStatusCode.NotFound,
                        Error = $"Ingredient with id {id} not found."
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
    /// Create a new ingredient.
    /// </summary>
    /// <param name="ingredient">The ingredient data.</param>
    /// <returns>The created ingredient.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.Ingredient), StatusCodes.Status201Created)]
    public async Task<ActionResult<v1_0.Ingredient>> PostIngredient(v1_0.Ingredient ingredient)
    {
        ingredient.Id = Guid.NewGuid();
        businessLogic.Ingredients.Add(_mapper.Map(ingredient)!);
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetIngredient", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = ingredient.Id
        }, ingredient);
    }
    
    /// <summary>
    /// Delete a specific ingredient by id.
    /// </summary>
    /// <param name="id">The id of the ingredient to delete.</param>
    /// <returns>A status indicating the result of the delete operation.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteIngredient(Guid id)
    {
        Ingredient? ingredient = await businessLogic.Ingredients.FindAsync(id);
        if (ingredient == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Ingredient with id {id} not found."
                });
        }

        await businessLogic.Ingredients.RemoveAsync(ingredient);
        await businessLogic.SaveChangesAsync();
        return NoContent();
    }
}