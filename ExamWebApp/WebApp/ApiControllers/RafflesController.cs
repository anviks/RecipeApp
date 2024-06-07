using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
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
[Authorize(Policy = "RafflePolicy")]
[ApiController]
public class RafflesController(IAppUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<App.DAL.DTO.Raffle, Raffle> _mapper = new(mapper);
    
    // GET: api/Raffles
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Raffle>>> GetRaffles()
    {
        var raffles = await unitOfWork.Raffles.FindAllAsync();
        return raffles.Select(_mapper.Map).ToList()!;
    }

    // GET: api/Raffles/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Raffle>> GetRaffle(Guid id)
    {
        var raffle = await unitOfWork.Raffles.FindAsync(id);

        if (raffle == null)
        {
            return NotFound();
        }

        return _mapper.Map(raffle)!;
    }

    // PUT: api/Raffles/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRaffle(Guid id, Raffle raffle)
    {
        if (id != raffle.Id)
        {
            return BadRequest();
        }

        unitOfWork.Raffles.Update(_mapper.Map(raffle)!);

        try
        {
            await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await unitOfWork.Raffles.ExistsAsync(id))
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

    // POST: api/Raffles
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Raffle>> PostRaffle(Raffle raffle)
    {
        unitOfWork.Raffles.Add(_mapper.Map(raffle)!);
        await unitOfWork.SaveChangesAsync();

        return CreatedAtAction("GetRaffle", new { id = raffle.Id }, raffle);
    }

    // DELETE: api/Raffles/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRaffle(Guid id)
    {
        var raffle = await unitOfWork.Raffles.FindAsync(id);
        if (raffle == null)
        {
            return NotFound();
        }

        unitOfWork.Raffles.Remove(raffle);
        await unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}