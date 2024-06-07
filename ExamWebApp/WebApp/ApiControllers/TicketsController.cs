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
public class TicketsController(IAppUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<App.DAL.DTO.Ticket, Ticket> _mapper = new(mapper);
    
    // GET: api/Tickets
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
    {
        var tickets = await unitOfWork.Tickets.FindAllAsync();
        return tickets.Select(_mapper.Map).ToList()!;
    }

    // GET: api/Tickets/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Ticket>> GetTicket(Guid id)
    {
        var ticket = await unitOfWork.Tickets.FindAsync(id);

        if (ticket == null)
        {
            return NotFound();
        }

        return _mapper.Map(ticket)!;
    }

    // PUT: api/Tickets/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTicket(Guid id, Ticket ticket)
    {
        if (id != ticket.Id)
        {
            return BadRequest();
        }

        unitOfWork.Tickets.Update(_mapper.Map(ticket)!);

        try
        {
            await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await unitOfWork.Tickets.ExistsAsync(id))
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

    // POST: api/Tickets
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
    {
        unitOfWork.Tickets.Add(_mapper.Map(ticket)!);
        await unitOfWork.SaveChangesAsync();

        return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
    }

    // DELETE: api/Tickets/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(Guid id)
    {
        var ticket = await unitOfWork.Tickets.FindAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }

        await unitOfWork.Tickets.RemoveAsync(ticket);
        await unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}