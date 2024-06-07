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

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class CompaniesController(IAppUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<App.DAL.DTO.Company, Company> _mapper = new(mapper);
    
    // GET: api/Companies
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
    {
        var companies = await unitOfWork.Companies.FindAllAsync();
        return companies.Select(_mapper.Map).ToList()!;
    }

    // GET: api/Companies/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Company>> GetCompany(Guid id)
    {
        var company = await unitOfWork.Companies.FindAsync(id);

        if (company == null)
        {
            return NotFound();
        }

        return _mapper.Map(company)!;
    }

    // PUT: api/Companies/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompany(Guid id, Company company)
    {
        if (id != company.Id)
        {
            return BadRequest();
        }

        unitOfWork.Companies.Update(_mapper.Map(company)!);

        try
        {
            await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await unitOfWork.Companies.ExistsAsync(id))
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

    // POST: api/Companies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Company>> PostCompany(Company company)
    {
        unitOfWork.Companies.Add(_mapper.Map(company)!);
        await unitOfWork.SaveChangesAsync();

        return CreatedAtAction("GetCompany", new { id = company.Id }, company);
    }

    // DELETE: api/Companies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        var company = await unitOfWork.Companies.FindAsync(id);
        if (company == null)
        {
            return NotFound();
        }

        await unitOfWork.Companies.RemoveAsync(company);
        await unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}