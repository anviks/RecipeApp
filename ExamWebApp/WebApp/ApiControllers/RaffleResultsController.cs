using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DTO.v1_0;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using App.DAL.Contracts;
using AutoMapper;
using Helpers;

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class RaffleResultsController(IAppUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<App.DAL.DTO.RaffleResult, RaffleResult> _mapper = new(mapper);
    
    // GET: api/RaffleResults
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<RaffleResult>>> GetRaffleResults()
    {
        var raffleResults = await unitOfWork.RaffleResults.FindAllAsync();
        return raffleResults.Select(_mapper.Map).ToList()!;
    }

    // GET: api/RaffleResults/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<RaffleResult>> GetRaffleResult(Guid id)
    {
        var raffleResult = await unitOfWork.RaffleResults.FindAsync(id);

        if (raffleResult == null)
        {
            return NotFound();
        }

        return _mapper.Map(raffleResult)!;
    }

    // PUT: api/RaffleResults/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRaffleResult(Guid id, RaffleResult raffleResult)
    {
        if (id != raffleResult.Id)
        {
            return BadRequest();
        }

        unitOfWork.RaffleResults.Update(_mapper.Map(raffleResult)!);

        try
        {
            await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await unitOfWork.RaffleResults.ExistsAsync(id))
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

    // POST: api/RaffleResults
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<RaffleResult>> PostRaffleResult(RaffleResult raffleResult)
    {
        unitOfWork.RaffleResults.Add(_mapper.Map(raffleResult)!);
        await unitOfWork.SaveChangesAsync();

        return CreatedAtAction("GetRaffleResult", new { id = raffleResult.Id }, raffleResult);
    }

    // DELETE: api/RaffleResults/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRaffleResult(Guid id)
    {
        var raffleResult = await unitOfWork.RaffleResults.FindAsync(id);
        if (raffleResult == null)
        {
            return NotFound();
        }

        await unitOfWork.RaffleResults.RemoveAsync(raffleResult);
        await unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}