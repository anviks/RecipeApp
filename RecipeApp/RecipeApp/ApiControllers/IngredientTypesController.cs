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
public class IngredientTypesController(AppDbContext context) : ControllerBase
{
    // GET: api/v1/IngredientTypes
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<IngredientType>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<IngredientType>>> GetIngredientTypes()
    {
        return await context.IngredientTypes.ToListAsync();
    }

    // GET: api/v1/IngredientTypes/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IngredientType>(StatusCodes.Status200OK)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IngredientType>> GetIngredientType(Guid id)
    {
        IngredientType? ingredientType = await context.IngredientTypes.FindAsync(id);

        if (ingredientType == null)
        {
            return NotFound();
        }

        return ingredientType;
    }

    // PUT: api/v1/IngredientTypes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutIngredientType(Guid id, IngredientType ingredientType)
    {
        if (id != ingredientType.Id)
        {
            return BadRequest();
        }

        context.Entry(ingredientType).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!IngredientTypeExists(id))
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

    // POST: api/v1/IngredientTypes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType<IngredientType>(StatusCodes.Status201Created)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IngredientType>> PostIngredientType(IngredientType ingredientType)
    {
        context.IngredientTypes.Add(ingredientType);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetIngredientType", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = ingredientType.Id
        }, ingredientType);
    }

    // DELETE: api/v1/IngredientTypes/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteIngredientType(Guid id)
    {
        IngredientType? ingredientType = await context.IngredientTypes.FindAsync(id);
        if (ingredientType == null)
        {
            return NotFound();
        }

        context.IngredientTypes.Remove(ingredientType);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool IngredientTypeExists(Guid id)
    {
        return context.IngredientTypes.Any(e => e.Id == id);
    }
}