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
/// API controller for managing ingredient type associations.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
[ApiController]
public class IngredientTypeAssociationsController(
    IAppBusinessLogic businessLogic,
    IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<IngredientTypeAssociation, v1_0.IngredientTypeAssociation> _mapper =
        new(mapper);
    
    /// <summary>
    /// Get all ingredient type associations.
    /// </summary>
    /// <returns>A list of ingredient type associations.</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<v1_0.IngredientTypeAssociation>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.IngredientTypeAssociation>>> GetIngredientTypeAssociations()
    {
        var ingredientTypeAssociations = await businessLogic.IngredientTypeAssociations.GetAllAsync();
        return Ok(ingredientTypeAssociations);
    }

    /// <summary>
    /// Get a specific ingredient type association by id.
    /// </summary>
    /// <param name="id">The id of the ingredient type association.</param>
    /// <returns>The ingredient type association with the specified id.</returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.IngredientTypeAssociation), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.IngredientTypeAssociation>> GetIngredientTypeAssociation(Guid id)
    {
        IngredientTypeAssociation? ingredientTypeAssociation =
            await businessLogic.IngredientTypeAssociations.GetByIdAsync(id);

        if (ingredientTypeAssociation == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"IngredientTypeAssociation with id {id} not found."
                });
        }

        return Ok(_mapper.Map(ingredientTypeAssociation));
    }

    /// <summary>
    /// Update a specific ingredient type association.
    /// </summary>
    /// <param name="id">The id of the ingredient type association to update.</param>
    /// <param name="ingredientTypeAssociation">The updated ingredient type association data.</param>
    /// <returns>A status indicating the result of the update operation.</returns>
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutIngredientTypeAssociation(Guid id,
        v1_0.IngredientTypeAssociation ingredientTypeAssociation)
    {
        if (id != ingredientTypeAssociation.Id)
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
            await businessLogic.IngredientTypeAssociations.UpdateAsync(_mapper.Map(ingredientTypeAssociation)!);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.IngredientTypeAssociations.ExistsAsync(id))
            {
                return NotFound(
                    new v1_0.RestApiErrorResponse
                    {
                        Status = HttpStatusCode.NotFound,
                        Error = $"IngredientTypeAssociation with id {id} not found."
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
    /// Create a new ingredient type association.
    /// </summary>
    /// <param name="ingredientTypeAssociation">The ingredient type association data.</param>
    /// <returns>The created ingredient type association.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.IngredientTypeAssociation), StatusCodes.Status201Created)]
    public async Task<ActionResult<v1_0.IngredientTypeAssociation>> PostIngredientTypeAssociation(
        v1_0.IngredientTypeAssociation ingredientTypeAssociation)
    {
        await businessLogic.IngredientTypeAssociations.AddAsync(_mapper.Map(ingredientTypeAssociation)!);
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetIngredientTypeAssociation", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = ingredientTypeAssociation.Id
        }, ingredientTypeAssociation);
    }

    /// <summary>
    /// Delete a specific ingredient type association by id.
    /// </summary>
    /// <param name="id">The id of the ingredient type association to delete.</param>
    /// <returns>A status indicating the result of the delete operation.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteIngredientTypeAssociation(Guid id)
    {
        IngredientTypeAssociation? ingredientTypeAssociation =
            await businessLogic.IngredientTypeAssociations.GetByIdAsync(id);
        if (ingredientTypeAssociation == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"IngredientTypeAssociation with id {id} not found."
                });
        }

        await businessLogic.IngredientTypeAssociations.DeleteAsync(ingredientTypeAssociation);
        await businessLogic.SaveChangesAsync();

        return NoContent();
    }
}