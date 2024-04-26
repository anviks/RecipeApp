using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RecipeApp.ApiControllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TestController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<string>>> Get()
    {
        return Ok(new List<string> { "value1", "value2" });
    }
}