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
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class ReviewsController(
    IAppBusinessLogic businessLogic,
    IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<v1_0.Review, BLL_DTO.Review> _mapper = new(mapper);   
    
    // GET: api/v1/Reviews
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<v1_0.Review>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.Review>>> GetReviews()
    {
        var reviews = await businessLogic.Reviews.FindAllAsync();
        return Ok(reviews.Select(_mapper.Map));
    }

    // GET: api/v1/Reviews/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<v1_0.Review>(StatusCodes.Status200OK)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.Review>> GetReview(Guid id)
    {
        BLL_DTO.Review? review = await businessLogic.Reviews.FindAsync(id);

        if (review == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Review with id {id} not found."
                });
        }

        return Ok(_mapper.Map(review));
    }

    // PUT: api/v1/Reviews/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutReview(Guid id, v1_0.Review review)
    {
        if (id != review.Id)
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
            businessLogic.Reviews.Update(_mapper.Map(review)!);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.Reviews.ExistsAsync(id))
            {
                return NotFound(
                    new v1_0.RestApiErrorResponse
                    {
                        Status = HttpStatusCode.NotFound,
                        Error = $"Review with id {id} not found."
                    });
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
    [ProducesResponseType<v1_0.Review>(StatusCodes.Status201Created)]
    public async Task<ActionResult<v1_0.Review>> PostReview(v1_0.Review review)
    {
        businessLogic.Reviews.Add(_mapper.Map(review)!);
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetReview", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = review.Id
        }, review);
    }

    // DELETE: api/v1/Reviews/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReview(Guid id)
    {
        BLL_DTO.Review? review = await businessLogic.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Review with id {id} not found."
                });
        }

        await businessLogic.Reviews.RemoveAsync(review);
        await businessLogic.SaveChangesAsync();

        return NoContent();
    }
}