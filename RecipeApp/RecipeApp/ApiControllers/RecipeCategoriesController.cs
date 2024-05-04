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
public class RecipeCategoriesController(AppDbContext context) : ControllerBase
{
    // GET: api/v1/RecipeCategories
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<RecipeCategory>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RecipeCategory>>> GetRecipeCategories()
    {
        return await context.RecipeCategories.ToListAsync();
    }

    // GET: api/v1/RecipeCategories/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<RecipeCategory>(StatusCodes.Status200OK)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecipeCategory>> GetRecipeCategory(Guid id)
    {
        RecipeCategory? recipeCategory = await context.RecipeCategories.FindAsync(id);

        if (recipeCategory == null)
        {
            return NotFound();
        }

        return recipeCategory;
    }

    // PUT: api/v1/RecipeCategories/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutRecipeCategory(Guid id, RecipeCategory recipeCategory)
    {
        if (id != recipeCategory.Id)
        {
            return BadRequest();
        }

        context.Entry(recipeCategory).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RecipeCategoryExists(id))
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

    // POST: api/v1/RecipeCategories
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType<RecipeCategory>(StatusCodes.Status201Created)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RecipeCategory>> PostRecipeCategory(RecipeCategory recipeCategory)
    {
        context.RecipeCategories.Add(recipeCategory);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetRecipeCategory", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = recipeCategory.Id
        }, recipeCategory);
    }

    // DELETE: api/v1/RecipeCategories/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecipeCategory(Guid id)
    {
        RecipeCategory? recipeCategory = await context.RecipeCategories.FindAsync(id);
        if (recipeCategory == null)
        {
            return NotFound();
        }

        context.RecipeCategories.Remove(recipeCategory);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool RecipeCategoryExists(Guid id)
    {
        return context.RecipeCategories.Any(e => e.Id == id);
    }
}