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
public class RecipesController(AppDbContext context) : ControllerBase
{
    // GET: api/v1/Recipes
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<Recipe>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
    {
        return await context.Recipes.ToListAsync();
    }

    // GET: api/v1/Recipes/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<Recipe>(StatusCodes.Status200OK)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Recipe>> GetRecipe(Guid id)
    {
        Recipe? recipe = await context.Recipes.FindAsync(id);

        if (recipe == null)
        {
            return NotFound();
        }

        return recipe;
    }

    // PUT: api/v1/Recipes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutRecipe(Guid id, Recipe recipe)
    {
        if (id != recipe.Id)
        {
            return BadRequest();
        }

        context.Entry(recipe).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RecipeExists(id))
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

    // POST: api/v1/Recipes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType<Recipe>(StatusCodes.Status201Created)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
    {
        context.Recipes.Add(recipe);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetRecipe", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = recipe.Id
        }, recipe);
    }

    // DELETE: api/v1/Recipes/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecipe(Guid id)
    {
        Recipe? recipe = await context.Recipes.FindAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }

        context.Recipes.Remove(recipe);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool RecipeExists(Guid id)
    {
        return context.Recipes.Any(e => e.Id == id);
    }
}