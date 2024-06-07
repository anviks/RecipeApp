using App.DAL.Contracts;
using App.DAL.EF;
using App.DAL.DTO;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
[ApiController]
public class SamplesController(IAppUnitOfWork unitOfWork) : ControllerBase
{
    // GET: api/Samples
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Sample>>> GetSamples()
    {
        return Ok(await unitOfWork.Samples.FindAllAsync());
    }

    // GET: api/Samples/5
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<Sample>> GetSample(Guid id)
    {
        Sample? sample = await unitOfWork.Samples.FindAsync(id);

        if (sample == null)
        {
            return NotFound();
        }

        return sample;
    }

    // PUT: api/Samples/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutSample(Guid id, Sample sample)
    {
        if (id != sample.Id)
        {
            return BadRequest();
        }

        unitOfWork.Samples.Update(sample);

        try
        {
            await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await unitOfWork.Samples.ExistsAsync(id))
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

    // POST: api/Samples
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Sample>> PostSample(Sample sample)
    {
        unitOfWork.Samples.Add(sample);
        await unitOfWork.SaveChangesAsync();

        return CreatedAtAction("GetSample", new { id = sample.Id }, sample);
    }

    // DELETE: api/Samples/5
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSample(Guid id)
    {
        Sample? sample = await unitOfWork.Samples.FindAsync(id);
        if (sample == null)
        {
            return NotFound();
        }

        await unitOfWork.Samples.RemoveAsync(sample);
        await unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}