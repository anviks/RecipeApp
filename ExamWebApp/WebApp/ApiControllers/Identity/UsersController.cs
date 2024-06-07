using App.DAL.Contracts;
using App.DTO.v1_0.Identity;
using Asp.Versioning;
using AutoMapper;
using Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers.Identity;


[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class UsersController(IAppUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly EntityMapper<App.Domain.Identity.AppUser, AppUser> _mapper = new(mapper);
    
    // GET: api/Users
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await unitOfWork.Users.FindAllAsync();
        return users.Select(_mapper.Map).ToList()!;
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<AppUser>> GetUser(Guid id)
    {
        var user = await unitOfWork.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return _mapper.Map(user)!;
    }
}