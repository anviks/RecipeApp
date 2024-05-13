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
public class RecipeIngredientsController(
    IAppBusinessLogic businessLogic,
    IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<v1_0.RecipeIngredient, BLL_DTO.RecipeIngredient> _mapper = new(mapper);
    
    // GET: api/v1/RecipeIngredients
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<v1_0.RecipeIngredient>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.RecipeIngredient>>> GetRecipeIngredients()
    {
        var recipeIngredients = await businessLogic.RecipeIngredients.FindAllAsync();
        return Ok(recipeIngredients.Select(_mapper.Map));
    }

    // GET: api/v1/RecipeIngredients/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<v1_0.RecipeIngredient>(StatusCodes.Status200OK)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.RecipeIngredient>> GetRecipeIngredient(Guid id)
    {
        BLL_DTO.RecipeIngredient? recipeIngredient = await businessLogic.RecipeIngredients.FindAsync(id);

        if (recipeIngredient == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"RecipeIngredient with id {id} not found."
                });
        }

        return Ok(_mapper.Map(recipeIngredient));
    }

    // PUT: api/v1/RecipeIngredients/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutRecipeIngredient(Guid id, v1_0.RecipeIngredient recipeIngredient)
    {
        if (id != recipeIngredient.Id)
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
            businessLogic.RecipeIngredients.Update(_mapper.Map(recipeIngredient)!);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.RecipeIngredients.ExistsAsync(id))
            {
                return NotFound(
                    new v1_0.RestApiErrorResponse
                    {
                        Status = HttpStatusCode.NotFound,
                        Error = $"RecipeIngredient with id {id} not found."
                    });
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
    [ProducesResponseType<v1_0.RecipeIngredient>(StatusCodes.Status201Created)]
    public async Task<ActionResult<v1_0.RecipeIngredient>> PostRecipeIngredient(v1_0.RecipeIngredient recipeIngredient)
    {
        businessLogic.RecipeIngredients.Add(_mapper.Map(recipeIngredient)!);
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetRecipeIngredient", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = recipeIngredient.Id
        }, recipeIngredient);
    }

    // DELETE: api/v1/RecipeIngredients/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecipeIngredient(Guid id)
    {
        BLL_DTO.RecipeIngredient? recipeIngredient = await businessLogic.RecipeIngredients.FindAsync(id);
        if (recipeIngredient == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"RecipeIngredient with id {id} not found."
                });
        }

        await businessLogic.RecipeIngredients.RemoveAsync(recipeIngredient);
        await businessLogic.SaveChangesAsync();

        return NoContent();
    }
}