using System.Net;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Contracts;
using RecipeApp.Application.DTO;
using RecipeApp.Base.Helpers;
using RecipeApp.Infrastructure.Data.EntityFramework.Entities.Identity;
using v1_0 = RecipeApp.Web.DTO.v1_0;

namespace RecipeApp.Web.ApiControllers;

/// <summary>
/// API controller for managing reviews.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class ReviewsController(
    IAppBusinessLogic businessLogic,
    IMapper mapper,
    UserManager<AppUser> userManager) : ControllerBase
{
    private readonly EntityMapper<v1_0.ReviewResponse, ReviewResponse> _responseLayerMapper = new(mapper);
    private readonly EntityMapper<v1_0.ReviewRequest, ReviewRequest> _requestLayerMapper = new(mapper);
    
    /// <summary>
    /// Get all reviews.
    /// </summary>
    /// <returns>A list of reviews.</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<v1_0.ReviewResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.ReviewResponse>>> GetReviews()
    {
        var reviews = await businessLogic.Reviews.FindAllAsync();
        return Ok(reviews.Select(_responseLayerMapper.Map));
    }

    /// <summary>
    /// Get a specific review by id.
    /// </summary>
    /// <param name="id">The id of the review.</param>
    /// <returns>The review with the specified id.</returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.ReviewResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.ReviewResponse>> GetReview(Guid id)
    {
        ReviewResponse? review = await businessLogic.Reviews.FindAsync(id);

        if (review == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Review with id {id} not found."
                });
        }

        return Ok(_responseLayerMapper.Map(review));
    }

    /// <summary>
    /// Update a specific review.
    /// </summary>
    /// <param name="id">The id of the review to update.</param>
    /// <param name="reviewRequest">The updated review data.</param>
    /// <returns>A status indicating the result of the update operation.</returns>
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutReview(Guid id, v1_0.ReviewRequest reviewRequest)
    {
        if (id != reviewRequest.Id)
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
            await businessLogic.Reviews.UpdateAsync(_requestLayerMapper.Map(reviewRequest)!);
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

    /// <summary>
    /// Create a new review.
    /// </summary>
    /// <param name="reviewRequest">The review data.</param>
    /// <returns>The created review.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.ReviewResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<v1_0.ReviewResponse>> PostReview(v1_0.ReviewRequest reviewRequest)
    {
        businessLogic.Reviews.Add(_requestLayerMapper.Map(reviewRequest)!, Guid.Parse(userManager.GetUserId(User)!));
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetReview", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = reviewRequest.Id
        }, reviewRequest);
    }

    /// <summary>
    /// Delete a specific review by id.
    /// </summary>
    /// <param name="id">The id of the review to delete.</param>
    /// <returns>A status indicating the result of the delete operation.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReview(Guid id)
    {
        ReviewResponse? review = await businessLogic.Reviews.FindAsync(id);
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
