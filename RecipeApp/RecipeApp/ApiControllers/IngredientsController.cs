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
public class IngredientsController(AppDbContext context) : ControllerBase
{
    // GET: api/v1/Ingredients
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<Ingredient>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
    {
        return await context.Ingredients.ToListAsync();
    }

    // GET: api/v1/Ingredients/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<Ingredient>(StatusCodes.Status200OK)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Ingredient>> GetIngredient(Guid id)
    {
        Ingredient? ingredient = await context.Ingredients.FindAsync(id);

        if (ingredient == null)
        {
            return NotFound();
        }

        return ingredient;
    }

    // PUT: api/v1/Ingredients/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutIngredient(Guid id, Ingredient ingredient)
    {
        if (id != ingredient.Id)
        {
            return BadRequest();
        }

        context.Entry(ingredient).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!IngredientExists(id))
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

    // POST: api/v1/Ingredients
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType<Ingredient>(StatusCodes.Status201Created)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
    {
        context.Ingredients.Add(ingredient);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetIngredient", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = ingredient.Id
        }, ingredient);
    }

    // DELETE: api/v1/Ingredients/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteIngredient(Guid id)
    {
        Ingredient? ingredient = await context.Ingredients.FindAsync(id);
        if (ingredient == null)
        {
            return NotFound();
        }

        context.Ingredients.Remove(ingredient);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool IngredientExists(Guid id)
    {
        return context.Ingredients.Any(e => e.Id == id);
    }
}