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
public class ReviewsController(AppDbContext context) : ControllerBase
{
    // GET: api/v1/Reviews
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<Review>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
    {
        return await context.Reviews.ToListAsync();
    }

    // GET: api/v1/Reviews/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<Review>(StatusCodes.Status200OK)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Review>> GetReview(Guid id)
    {
        Review? review = await context.Reviews.FindAsync(id);

        if (review == null)
        {
            return NotFound();
        }

        return review;
    }

    // PUT: api/v1/Reviews/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutReview(Guid id, Review review)
    {
        if (id != review.Id)
        {
            return BadRequest();
        }

        context.Entry(review).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ReviewExists(id))
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

    // POST: api/v1/Reviews
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType<Review>(StatusCodes.Status201Created)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Review>> PostReview(Review review)
    {
        context.Reviews.Add(review);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetReview", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = review.Id
        }, review);
    }

    // DELETE: api/v1/Reviews/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReview(Guid id)
    {
        Review? review = await context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound();
        }

        context.Reviews.Remove(review);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool ReviewExists(Guid id)
    {
        return context.Reviews.Any(e => e.Id == id);
    }
}