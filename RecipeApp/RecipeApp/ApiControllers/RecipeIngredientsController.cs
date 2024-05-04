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
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class RecipeIngredientsController(AppDbContext context) : ControllerBase
{
    // GET: api/v1/RecipeIngredients
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<RecipeIngredient>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RecipeIngredient>>> GetRecipeIngredients()
    {
        return await context.RecipeIngredients.ToListAsync();
    }

    // GET: api/v1/RecipeIngredients/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<RecipeIngredient>(StatusCodes.Status200OK)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecipeIngredient>> GetRecipeIngredient(Guid id)
    {
        RecipeIngredient? recipeIngredient = await context.RecipeIngredients.FindAsync(id);

        if (recipeIngredient == null)
        {
            return NotFound();
        }

        return recipeIngredient;
    }

    // PUT: api/v1/RecipeIngredients/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutRecipeIngredient(Guid id, RecipeIngredient recipeIngredient)
    {
        if (id != recipeIngredient.Id)
        {
            return BadRequest();
        }

        context.Entry(recipeIngredient).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RecipeIngredientExists(id))
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

    // POST: api/v1/RecipeIngredients
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType<RecipeIngredient>(StatusCodes.Status201Created)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RecipeIngredient>> PostRecipeIngredient(RecipeIngredient recipeIngredient)
    {
        context.RecipeIngredients.Add(recipeIngredient);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetRecipeIngredient", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = recipeIngredient.Id
        }, recipeIngredient);
    }

    // DELETE: api/v1/RecipeIngredients/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecipeIngredient(Guid id)
    {
        RecipeIngredient? recipeIngredient = await context.RecipeIngredients.FindAsync(id);
        if (recipeIngredient == null)
        {
            return NotFound();
        }

        context.RecipeIngredients.Remove(recipeIngredient);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool RecipeIngredientExists(Guid id)
    {
        return context.RecipeIngredients.Any(e => e.Id == id);
    }
}