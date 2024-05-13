using System.Net;
using App.Contracts.BLL;
using Asp.Versioning;
using AutoMapper;
using Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLL_DTO = App.BLL.DTO;
using v1_0 = App.DTO.v1_0;

namespace RecipeApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
[ApiController]
public class UnitsController(
    IAppBusinessLogic businessLogic,
    IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<v1_0.Unit, BLL_DTO.Unit> _mapper = new(mapper);
    
    // GET: api/v1/Units
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<v1_0.Unit>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.Unit>>> GetUnits()
    {
        var units = await businessLogic.Units.FindAllAsync();
        return Ok(units.Select(_mapper.Map));
    }

    // GET: api/v1/Units/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<v1_0.Unit>(StatusCodes.Status200OK)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.Unit>> GetUnit(Guid id)
    {
        BLL_DTO.Unit? unit = await businessLogic.Units.FindAsync(id);

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

    // PUT: api/v1/Units/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
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
            businessLogic.Units.Update(_mapper.Map(unit)!);
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

    // POST: api/v1/Units
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType<v1_0.Unit>(StatusCodes.Status201Created)]
    public async Task<ActionResult<v1_0.Unit>> PostUnit(v1_0.Unit unit)
    {
        businessLogic.Units.Add(_mapper.Map(unit)!);
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetUnit", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = unit.Id
        }, unit);
    }

    // DELETE: api/v1/Units/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUnit(Guid id)
    {
        BLL_DTO.Unit? unit = await businessLogic.Units.FindAsync(id);
        if (unit == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Unit with id {id} not found."
                });
        }

        await businessLogic.Units.RemoveAsync(unit);
        await businessLogic.SaveChangesAsync();

        return NoContent();
    }
}