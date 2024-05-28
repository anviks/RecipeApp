using System.Globalization;

namespace RecipeApp.Helpers;

public class CustomCultureMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            // Set a specific culture for every request
            var defaultCulture = new CultureInfo("en-GB");

            CultureInfo.CurrentCulture = defaultCulture;
            CultureInfo.CurrentUICulture = defaultCulture;
        }
        
        await next(context);
    }
}