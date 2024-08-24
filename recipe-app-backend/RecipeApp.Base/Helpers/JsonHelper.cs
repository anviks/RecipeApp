using System.Text.Json;

namespace RecipeApp.Base.Helpers;

public static class JsonHelper
{
    public static readonly JsonSerializerOptions CamelCase = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
}