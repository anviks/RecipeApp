using System.Net;

namespace RecipeApp.Web.DTO.v1_0;

public class RestApiErrorResponse
{
    public HttpStatusCode Status { get; set; }
    public string Error { get; set; } = default!;
}
