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
[ApiController]
public class PrizesController(IAppUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<App.DAL.DTO.Prize, Prize> _mapper = new(mapper);
    
    // GET: api/Prizes
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Prize>>> GetPrizes()
    {
        var prizes = await unitOfWork.Prizes.FindAllAsync();
        return prizes.Select(_mapper.Map).ToList()!;
    }

    // GET: api/Prizes/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Prize>> GetPrize(Guid id)
    {
        var prize = await unitOfWork.Prizes.FindAsync(id);

        if (prize == null)
        {
            return NotFound();
        }

        return _mapper.Map(prize)!;
    }

    // PUT: api/Prizes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPrize(Guid id, Prize prize)
    {
        if (id != prize.Id)
        {
            return BadRequest();
        }

        unitOfWork.Prizes.Update(_mapper.Map(prize)!);

        try
        {
            await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await unitOfWork.Prizes.ExistsAsync(id))
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

    // POST: api/Prizes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Prize>> PostPrize(Prize prize)
    {
        unitOfWork.Prizes.Add(_mapper.Map(prize)!);
        await unitOfWork.SaveChangesAsync();

        return CreatedAtAction("GetPrize", new { id = prize.Id }, prize);
    }

    // DELETE: api/Prizes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrize(Guid id)
    {
        var prize = await unitOfWork.Prizes.FindAsync(id);
        if (prize == null)
        {
            return NotFound();
        }

        await unitOfWork.Prizes.RemoveAsync(prize);
        await unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}