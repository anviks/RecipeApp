using App.DAL.EF;
using App.Domain;
using App.DTO.v1_0;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
[ApiController]
public class UnitsController(AppDbContext context) : ControllerBase
{
    // GET: api/v1/Units
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<Unit>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
    {
        return await context.Units.ToListAsync();
    }

    // GET: api/v1/Units/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<Unit>(StatusCodes.Status200OK)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Unit>> GetUnit(Guid id)
    {
        Unit? unit = await context.Units.FindAsync(id);

        if (unit == null)
        {
            return NotFound();
        }

        return unit;
    }

    // PUT: api/v1/Units/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutUnit(Guid id, Unit unit)
    {
        if (id != unit.Id)
        {
            return BadRequest();
        }

        context.Entry(unit).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UnitExists(id))
            {
                return NotFound();
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
    [ProducesResponseType<Unit>(StatusCodes.Status201Created)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Unit>> PostUnit(Unit unit)
    {
        context.Units.Add(unit);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetUnit", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = unit.Id
        }, unit);
    }

    // DELETE: api/v1/Units/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUnit(Guid id)
    {
        Unit? unit = await context.Units.FindAsync(id);
        if (unit == null)
        {
            return NotFound();
        }

        context.Units.Remove(unit);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool UnitExists(Guid id)
    {
        return context.Units.Any(e => e.Id == id);
    }
}