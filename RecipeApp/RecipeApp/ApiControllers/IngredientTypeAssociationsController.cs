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
public class IngredientTypeAssociationsController(
    IAppBusinessLogic businessLogic,
    IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<BLL_DTO.IngredientTypeAssociation, v1_0.IngredientTypeAssociation> _mapper = new(mapper);
    
    // GET: api/v1/IngredientTypeAssociations
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<IEnumerable<v1_0.IngredientTypeAssociation>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<v1_0.IngredientTypeAssociation>>> GetIngredientTypeAssociations()
    {
        var ingredientTypeAssociations = await businessLogic.IngredientTypeAssociations.FindAllAsync();
        return Ok(ingredientTypeAssociations);
    }

    // GET: api/v1/IngredientTypeAssociations/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType<v1_0.IngredientTypeAssociation>(StatusCodes.Status200OK)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<v1_0.IngredientTypeAssociation>> GetIngredientTypeAssociation(Guid id)
    {
        BLL_DTO.IngredientTypeAssociation? ingredientTypeAssociation = await businessLogic.IngredientTypeAssociations.FindAsync(id);

        if (ingredientTypeAssociation == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"IngredientTypeAssociation with ID {id} not found."
                });
        }

        return Ok(_mapper.Map(ingredientTypeAssociation));
    }

    // PUT: api/v1/IngredientTypeAssociations/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutIngredientTypeAssociation(Guid id, v1_0.IngredientTypeAssociation ingredientTypeAssociation)
    {
        if (id != ingredientTypeAssociation.Id)
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
            businessLogic.IngredientTypeAssociations.Update(_mapper.Map(ingredientTypeAssociation)!);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.IngredientTypeAssociations.ExistsAsync(id))
            {
                return NotFound(
                    new v1_0.RestApiErrorResponse
                    {
                        Status = HttpStatusCode.NotFound,
                        Error = $"IngredientTypeAssociation with ID {id} not found."
                    });
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/v1/IngredientTypeAssociations
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType<v1_0.IngredientTypeAssociation>(StatusCodes.Status201Created)]
    public async Task<ActionResult<v1_0.IngredientTypeAssociation>> PostIngredientTypeAssociation(v1_0.IngredientTypeAssociation ingredientTypeAssociation)
    {
        businessLogic.IngredientTypeAssociations.Add(_mapper.Map(ingredientTypeAssociation)!);
        await businessLogic.SaveChangesAsync();

        return CreatedAtAction("GetIngredientTypeAssociation", new
        {
            version = HttpContext.GetRequestedApiVersion()?.ToString(),
            id = ingredientTypeAssociation.Id
        }, ingredientTypeAssociation);
    }

    // DELETE: api/v1/IngredientTypeAssociations/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<v1_0.RestApiErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteIngredientTypeAssociation(Guid id)
    {
        BLL_DTO.IngredientTypeAssociation? ingredientTypeAssociation = await businessLogic.IngredientTypeAssociations.FindAsync(id);
        if (ingredientTypeAssociation == null)
        {
            return NotFound(
                new v1_0.RestApiErrorResponse
                {
                    Status = HttpStatusCode.NotFound,
                    Error = $"IngredientTypeAssociation with ID {id} not found."
                });
        }

        await businessLogic.IngredientTypeAssociations.RemoveAsync(ingredientTypeAssociation);
        await businessLogic.SaveChangesAsync();

        return NoContent();
    }
}