using System.Net;
using App.Contracts.BLL;
using BLL_DTO = App.BLL.DTO;
using v1_0 = App.DTO.v1_0;
using Asp.Versioning;
using AutoMapper;
using Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
[ApiController]
public class IngredientTypesController(
    IAppBusinessLogic businessLogic, 
    IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<BLL_DTO.IngredientType, v1_0.IngredientType> _mapper = new(mapper);
    
    // GET: api/v1/IngredientTypes
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<v1_0.IngredientType>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.IngredientType>>> GetIngredientTypes()
    {
        var ingredientTypes = await businessLogic.IngredientTypes.FindAllAsync();
        return Ok(ingredientTypes);
    }

    // GET: api/v1/IngredientTypes/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<v1_0.IngredientType>(StatusCodes.Status200OK)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.IngredientType>> GetIngredientType(Guid id)
    {
        BLL_DTO.IngredientType? ingredientType = await businessLogic.IngredientTypes.FindAsync(id);

        if (ingredientType == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"IngredientType with ID {id} not found."
                });
        }

        return Ok(_mapper.Map(ingredientType));
    }

    // PUT: api/v1/IngredientTypes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutIngredientType(Guid id, v1_0.IngredientType ingredientType)
    {
        if (id != ingredientType.Id)
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
            businessLogic.IngredientTypes.Update(_mapper.Map(ingredientType)!);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.IngredientTypes.ExistsAsync(id))
            {
                return NotFound(
                    new v1_0.RestApiErrorResponse
                    {
                        Status = HttpStatusCode.NotFound,
                        Error = $"IngredientType with ID {id} not found."
                    });
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/v1/IngredientTypes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType<v1_0.IngredientType>(StatusCodes.Status201Created)]
    public async Task<ActionResult<v1_0.IngredientType>> PostIngredientType(v1_0.IngredientType ingredientType)
    {
        BLL_DTO.IngredientType type = _mapper.Map(ingredientType)!;
        businessLogic.IngredientTypes.Add(type);
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetIngredientType", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            // TODO: Check if id is set in the DTO
            id = type.Id
        }, ingredientType);
    }

    // DELETE: api/v1/IngredientTypes/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteIngredientType(Guid id)
    {
        BLL_DTO.IngredientType? ingredientType = await businessLogic.IngredientTypes.FindAsync(id);
        if (ingredientType == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"IngredientType with ID {id} not found."
                });
        }

        await businessLogic.IngredientTypes.RemoveAsync(ingredientType);
        await businessLogic.SaveChangesAsync();

        return NoContent();
    }
}