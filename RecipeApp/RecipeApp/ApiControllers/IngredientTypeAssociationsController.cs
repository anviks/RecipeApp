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
public class IngredientTypeAssociationsController(AppDbContext context) : ControllerBase
{
    // GET: api/v1/IngredientTypeAssociations
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<IngredientTypeAssociation>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<IngredientTypeAssociation>>> GetIngredientTypeAssociations()
    {
        return await context.IngredientTypeAssociations.ToListAsync();
    }

    // GET: api/v1/IngredientTypeAssociations/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IngredientTypeAssociation>(StatusCodes.Status200OK)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IngredientTypeAssociation>> GetIngredientTypeAssociation(Guid id)
    {
        IngredientTypeAssociation? ingredientTypeAssociation = await context.IngredientTypeAssociations.FindAsync(id);

        if (ingredientTypeAssociation == null)
        {
            return NotFound();
        }

        return ingredientTypeAssociation;
    }

    // PUT: api/v1/IngredientTypeAssociations/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutIngredientTypeAssociation(Guid id, IngredientTypeAssociation ingredientTypeAssociation)
    {
        if (id != ingredientTypeAssociation.Id)
        {
            return BadRequest();
        }

        context.Entry(ingredientTypeAssociation).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!IngredientTypeAssociationExists(id))
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

    // POST: api/v1/IngredientTypeAssociations
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType<IngredientTypeAssociation>(StatusCodes.Status201Created)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IngredientTypeAssociation>> PostIngredientTypeAssociation(IngredientTypeAssociation ingredientTypeAssociation)
    {
        context.IngredientTypeAssociations.Add(ingredientTypeAssociation);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetIngredientTypeAssociation", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = ingredientTypeAssociation.Id
        }, ingredientTypeAssociation);
    }

    // DELETE: api/v1/IngredientTypeAssociations/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteIngredientTypeAssociation(Guid id)
    {
        IngredientTypeAssociation? ingredientTypeAssociation = await context.IngredientTypeAssociations.FindAsync(id);
        if (ingredientTypeAssociation == null)
        {
            return NotFound();
        }

        context.IngredientTypeAssociations.Remove(ingredientTypeAssociation);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool IngredientTypeAssociationExists(Guid id)
    {
        return context.IngredientTypeAssociations.Any(e => e.Id == id);
    }
}