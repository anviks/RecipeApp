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
/// API controller for managing units.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
[ApiController]
public class UnitsController(
    IAppBusinessLogic businessLogic,
    IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<v1_0.Unit, Unit> _mapper = new(mapper);
    
    /// <summary>
    /// Get all units.
    /// </summary>
    /// <returns>A list of units.</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<v1_0.Unit>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.Unit>>> GetUnits()
    {
        var units = await businessLogic.Units.GetAllAsync();
        return Ok(units.Select(_mapper.Map));
    }

    /// <summary>
    /// Get a specific unit by id.
    /// </summary>
    /// <param name="id">The id of the unit.</param>
    /// <returns>The unit with the specified id.</returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.Unit), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.Unit>> GetUnit(Guid id)
    {
        Unit? unit = await businessLogic.Units.GetByIdAsync(id);

        if (unit == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Unit with id {id} not found."
                });
        }

        return Ok(_mapper.Map(unit));
    }

    /// <summary>
    /// Update a specific unit.
    /// </summary>
    /// <param name="id">The id of the unit to update.</param>
    /// <param name="unit">The updated unit data.</param>
    /// <returns>A status indicating the result of the update operation.</returns>
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutUnit(Guid id, v1_0.Unit unit)
    {
        if (id != unit.Id)
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
            await businessLogic.Units.UpdateAsync(_mapper.Map(unit)!);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.Units.ExistsAsync(id))
            {
                return NotFound(
                    new v1_0.RestApiErrorResponse
                    {
                        Status = HttpStatusCode.NotFound,
                        Error = $"Unit with id {id} not found."
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
    /// Create a new unit.
    /// </summary>
    /// <param name="unit">The unit data.</param>
    /// <returns>The created unit.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.Unit), StatusCodes.Status201Created)]
    public async Task<ActionResult<v1_0.Unit>> PostUnit(v1_0.Unit unit)
    {
        await businessLogic.Units.AddAsync(_mapper.Map(unit)!);
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetUnit", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = unit.Id
        }, unit);
    }

    /// <summary>
    /// Delete a specific unit by id.
    /// </summary>
    /// <param name="id">The id of the unit to delete.</param>
    /// <returns>A status indicating the result of the delete operation.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUnit(Guid id)
    {
        Unit? unit = await businessLogic.Units.GetByIdAsync(id);
        if (unit == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Unit with id {id} not found."
                });
        }

        await businessLogic.Units.DeleteAsync(unit);
        await businessLogic.SaveChangesAsync();

        return NoContent();
    }
}