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
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
[ApiController]
public class IngredientsController(
    IAppBusinessLogic businessLogic,
    IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<v1_0.Ingredient, BLL_DTO.Ingredient> _mapper = new(mapper);

    // GET: api/v1/Ingredients
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<v1_0.Ingredient>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.Ingredient>>> GetIngredients()
    {
        var ingredients = await businessLogic.Ingredients.FindAllAsync();
        return Ok(ingredients.Select(_mapper.Map));
    }

    // GET: api/v1/Ingredients/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<v1_0.Ingredient>(StatusCodes.Status200OK)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.Ingredient>> GetIngredient(Guid id)
    {
        BLL_DTO.Ingredient? ingredient = await businessLogic.Ingredients.FindAsync(id);

        if (ingredient == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Ingredient with id {id} not found."
                });
        }

        return Ok(_mapper.Map(ingredient));
    }

    // PUT: api/v1/Ingredients/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task <IActionResult> PutIngredient(Guid id, v1_0.Ingredient ingredient)
    {
        if (id != ingredient.Id)
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
            businessLogic.Ingredients.Update(_mapper.Map(ingredient)!);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.Ingredients.ExistsAsync(id))
            {
                return NotFound(
                    new v1_0.RestApiErrorResponse
                    {
                        Status = HttpStatusCode.NotFound,
                        Error = $"Ingredient with id {id} not found."
                    });
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
    [ProducesResponseType<v1_0.Ingredient>(StatusCodes.Status201Created)]
    public async Task<ActionResult<v1_0.Ingredient>> PostIngredient(v1_0.Ingredient ingredient)
    {
        ingredient.Id = Guid.NewGuid();
        businessLogic.Ingredients.Add(_mapper.Map(ingredient)!);
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetIngredient", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = ingredient.Id
        }, ingredient);
    }

    // DELETE: api/v1/Ingredients/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteIngredient(Guid id)
    {
        BLL_DTO.Ingredient? ingredient = await businessLogic.Ingredients.FindAsync(id);
        if (ingredient == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"Ingredient with id {id} not found."
                });
        }

        await businessLogic.Ingredients.RemoveAsync(ingredient);
        await businessLogic.SaveChangesAsync();
        return NoContent();
    }
}