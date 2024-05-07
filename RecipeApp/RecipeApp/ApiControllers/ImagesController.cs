using System.Net;
using App.DTO.v1_0;
using Asp.Versioning;
using Helpers.Validation.File;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace RecipeApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class ImagesController(IWebHostEnvironment environment) : ControllerBase
{
    private readonly string _uploadPath = Path.Combine(environment.WebRootPath, "uploads", "images");

    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<RestApiErrorResponse>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Upload(
        [FromForm] 
        [FileSize(0, 10 * 1024 * 1024)]
        [AllowedExtensions([".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg"])]
        IFormFile file)
    {
        if (file == null)
        {
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = "No file uploaded."
            });
        }

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var uploadPath = Path.Combine(_uploadPath, fileName);
        await using (var stream = new FileStream(uploadPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var uploadUrl = "~/uploads/images/" + fileName;

        return Created(uploadUrl, new { imageUrl = uploadUrl });
    }
}