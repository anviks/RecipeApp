using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.DAL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DTO.v1_0;
using Asp.Versioning;
using AutoMapper;
using Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Activity = App.DTO.v1_0.Activity;

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class ActivityTypesController(IAppUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<App.DAL.DTO.ActivityType, ActivityType> _mapper = new(mapper);
    
    // GET: api/ActivityTypes
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ActivityType>>> GetActivityTypes()
    {
        var activityTypes = await unitOfWork.ActivityTypes.FindAllAsync();
        return activityTypes.Select(_mapper.Map).ToList()!;
    }

    // GET: api/ActivityTypes/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ActivityType>> GetActivityType(Guid id)
    {
        var activityType = await unitOfWork.ActivityTypes.FindAsync(id);

        if (activityType == null)
        {
            return NotFound();
        }

        return _mapper.Map(activityType)!;
    }

    // PUT: api/ActivityTypes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutActivityType(Guid id, ActivityType activityType)
    {
        if (id != activityType.Id)
        {
            return BadRequest();
        }

        unitOfWork.ActivityTypes.Update(_mapper.Map(activityType)!);

        try
        {
            await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await unitOfWork.ActivityTypes.ExistsAsync(id))
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

    // POST: api/ActivityTypes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ActivityType>> PostActivityType(ActivityType activityType)
    {
        unitOfWork.ActivityTypes.Add(_mapper.Map(activityType)!);
        await unitOfWork.SaveChangesAsync();

        return CreatedAtAction("GetActivityType", new { id = activityType.Id }, activityType);
    }

    // DELETE: api/ActivityTypes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivityType(Guid id)
    {
        var activityType = await unitOfWork.ActivityTypes.FindAsync(id);
        if (activityType == null)
        {
            return NotFound();
        }

        await unitOfWork.ActivityTypes.RemoveAsync(activityType);
        await unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}