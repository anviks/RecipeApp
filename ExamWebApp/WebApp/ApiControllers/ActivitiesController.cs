using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.DAL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DTO.v1_0;
using Asp.Versioning;
using AutoMapper;
using Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class ActivitiesController(IAppUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<App.DAL.DTO.Activity, Activity> _mapper = new(mapper);
    
    // GET: api/Activities
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
    {
        var activities = await unitOfWork.Activities.FindAllAsync();
        return activities.Select(_mapper.Map).ToList()!;
    }

    // GET: api/Activities/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Activity>> GetActivity(Guid id)
    {
        var activity = await unitOfWork.Activities.FindAsync(id);

        if (activity == null)
        {
            return NotFound();
        }

        return _mapper.Map(activity)!;
    }

    // PUT: api/Activities/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutActivity(Guid id, Activity activity)
    {
        if (id != activity.Id)
        {
            return BadRequest();
        }

        unitOfWork.Activities.Update(_mapper.Map(activity)!);

        try
        {
            await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await unitOfWork.Activities.ExistsAsync(id))
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

    // POST: api/Activities
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Consumes("application/json")]
    public async Task<ActionResult<Activity>> PostActivity(Activity activity)
    {
        unitOfWork.Activities.Add(_mapper.Map(activity)!);
        await unitOfWork.SaveChangesAsync();

        return CreatedAtAction("GetActivity", new { id = activity.Id }, activity);
    }

    // DELETE: api/Activities/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
        var activity = await unitOfWork.Activities.FindAsync(id);
        if (activity == null)
        {
            return NotFound();
        }

        await unitOfWork.Activities.RemoveAsync(activity);
        await unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}