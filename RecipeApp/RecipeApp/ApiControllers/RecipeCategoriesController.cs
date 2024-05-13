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
public class RecipeCategoriesController(
    IAppBusinessLogic businessLogic,
    IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<v1_0.RecipeCategory, BLL_DTO.RecipeCategory> _mapper = new(mapper);

    // GET: api/v1/RecipeCategories
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<v1_0.RecipeCategory>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.RecipeCategory>>> GetRecipeCategories()
    {
        var recipeCategories = await businessLogic.RecipeCategories.FindAllAsync();
        return Ok(recipeCategories.Select(_mapper.Map));
    }

    // GET: api/v1/RecipeCategories/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<v1_0.RecipeCategory>(StatusCodes.Status200OK)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.RecipeCategory>> GetRecipeCategory(Guid id)
    {
        BLL_DTO.RecipeCategory? recipeCategory = await businessLogic.RecipeCategories.FindAsync(id);

        if (recipeCategory == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"RecipeCategory with id {id} not found."
                });
        }

        return Ok(_mapper.Map(recipeCategory));
    }

    // PUT: api/v1/RecipeCategories/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutRecipeCategory(Guid id, v1_0.RecipeCategory recipeCategory)
    {
        if (id != recipeCategory.Id)
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
            businessLogic.RecipeCategories.Update(_mapper.Map(recipeCategory)!);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.RecipeCategories.ExistsAsync(id))
            {
                return NotFound(
                    new v1_0.RestApiErrorResponse
                    {
                        Status = HttpStatusCode.NotFound,
                        Error = $"RecipeCategory with id {id} not found."
                    });
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
    [ProducesResponseType<v1_0.RecipeCategory>(StatusCodes.Status201Created)]
    public async Task<ActionResult<v1_0.RecipeCategory>> PostRecipeCategory(v1_0.RecipeCategory recipeCategory)
    {
        businessLogic.RecipeCategories.Add(_mapper.Map(recipeCategory)!);
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetRecipeCategory", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = recipeCategory.Id
        }, recipeCategory);
    }

    // DELETE: api/v1/RecipeCategories/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecipeCategory(Guid id)
    {
        BLL_DTO.RecipeCategory? recipeCategory = await businessLogic.RecipeCategories.FindAsync(id);
        if (recipeCategory == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"RecipeCategory with id {id} not found."
                });
        }

        await businessLogic.RecipeCategories.RemoveAsync(recipeCategory);
        await businessLogic.SaveChangesAsync();

        return NoContent();
    }
}