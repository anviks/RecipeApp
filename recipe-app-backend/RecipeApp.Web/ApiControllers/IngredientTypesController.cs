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
/// API controller for managing ingredient types.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
[ApiController]
public class IngredientTypesController(
    IAppBusinessLogic businessLogic, 
    IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<IngredientType, v1_0.IngredientType> _mapper = new(mapper);
    
    /// <summary>
    /// Get all ingredient types.
    /// </summary>
    /// <returns>A list of ingredient types.</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<v1_0.IngredientType>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.IngredientType>>> GetIngredientTypes()
    {
        var ingredientTypes = await businessLogic.IngredientTypes.GetAllAsync();
        return Ok(ingredientTypes);
    }

    /// <summary>
    /// Get a specific ingredient type by id.
    /// </summary>
    /// <param name="id">The id of the ingredient type.</param>
    /// <returns>The ingredient type with the specified id.</returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.IngredientType), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.IngredientType>> GetIngredientType(Guid id)
    {
        IngredientType? ingredientType = await businessLogic.IngredientTypes.GetByIdAsync(id);

        if (ingredientType == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"IngredientType with id {id} not found."
                });
        }

        return Ok(_mapper.Map(ingredientType));
    }

    /// <summary>
    /// Update a specific ingredient type.
    /// </summary>
    /// <param name="id">The id of the ingredient type to update.</param>
    /// <param name="ingredientType">The updated ingredient type data.</param>
    /// <returns>A status indicating the result of the update operation.</returns>
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutIngredientType(Guid id, v1_0.IngredientType ingredientType)
    {
        if (id != ingredientType.Id)
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
            await businessLogic.IngredientTypes.UpdateAsync(_mapper.Map(ingredientType)!);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.IngredientTypes.ExistsAsync(id))
            {
                return NotFound(
                    new v1_0.RestApiErrorResponse
                    {
                        Status = HttpStatusCode.NotFound,
                        Error = $"IngredientType with id {id} not found."
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
    /// Create a new ingredient type.
    /// </summary>
    /// <param name="ingredientType">The ingredient type data.</param>
    /// <returns>The created ingredient type.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.IngredientType), StatusCodes.Status201Created)]
    public async Task<ActionResult<v1_0.IngredientType>> PostIngredientType(v1_0.IngredientType ingredientType)
    {
        IngredientType type = _mapper.Map(ingredientType)!;
        await businessLogic.IngredientTypes.AddAsync(type);
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetIngredientType", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = type.Id
        }, ingredientType);
    }

    /// <summary>
    /// Delete a specific ingredient type by id.
    /// </summary>
    /// <param name="id">The id of the ingredient type to delete.</param>
    /// <returns>A status indicating the result of the delete operation.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteIngredientType(Guid id)
    {
        IngredientType? ingredientType = await businessLogic.IngredientTypes.GetByIdAsync(id);
        if (ingredientType == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"IngredientType with id {id} not found."
                });
        }

        await businessLogic.IngredientTypes.DeleteAsync(ingredientType);
        await businessLogic.SaveChangesAsync();

        return NoContent();
    }
}
