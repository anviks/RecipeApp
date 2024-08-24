using System.Net;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Contracts;
using RecipeApp.Application.DTO;
using RecipeApp.Base.Helpers;
using v1_0 = RecipeApp.Web.DTO.v1_0;

namespace RecipeApp.Web.ApiControllers;

/// <summary>
/// API controller for managing categories.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
[ApiController]
public class CategoriesController(
    IAppBusinessLogic businessLogic, 
    IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<Category, v1_0.Category> _mapper = new(mapper);

    /// <summary>
    /// Get all categories.
    /// </summary>
    /// <returns>List of categories.</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<v1_0.Category>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.Category>>> GetCategories()
    {
        var categories = await businessLogic.Categories.FindAllAsync();
        return Ok(categories.Select(_mapper.Map).ToList());
    }

    /// <summary>
    /// Get a category by id.
    /// </summary>
    /// <param name="id">The id of the category.</param>
    /// <returns>The category.</returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.Category), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.Category>> GetCategory(Guid id)
    {
        Category? category = await businessLogic.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Category with id {id} not found."
                });
        }

        return Ok(_mapper.Map(category));
    }

    /// <summary>
    /// Update a category.
    /// </summary>
    /// <param name="id">The id of the category to update.</param>
    /// <param name="category">The updated category data.</param>
    /// <returns>A status indicating the result of the update operation.</returns>
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutCategory(Guid id, v1_0.Category category)
    {
        if (id != category.Id)
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
            businessLogic.Categories.Update(_mapper.Map(category)!);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.Categories.ExistsAsync(id))
            {
                return NotFound(
                    new v1_0.RestApiErrorResponse
                    {
                        Status = HttpStatusCode.NotFound,
                        Error = $"Category with id {id} not found."
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
    /// Create a category.
    /// </summary>
    /// <param name="category">The category to create.</param>
    /// <returns>The created category.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(v1_0.Category), StatusCodes.Status201Created)]
    public async Task<ActionResult<v1_0.Category>> PostCategory(v1_0.Category category)
    {
        category.Id = Guid.NewGuid();
        businessLogic.Categories.Add(_mapper.Map(category)!);
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetCategory", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = category.Id
        }, category);
    }

    /// <summary>
    /// Delete a category.
    /// </summary>
    /// <param name="id">The id of the category to delete.</param>
    /// <returns>A status indicating the result of the delete operation.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(v1_0.RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        Category? category = await businessLogic.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Category with id {id} not found."
                });
        }

        await businessLogic.Categories.RemoveAsync(category);
        await businessLogic.SaveChangesAsync();

        return NoContent();
    }
}