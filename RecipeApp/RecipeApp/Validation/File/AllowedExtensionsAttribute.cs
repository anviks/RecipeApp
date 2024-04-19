using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Validation.File;

public class AllowedExtensionsAttribute(string[] extensions) : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
    {
        if (value is not IFormFile file) return ValidationResult.Success;
        var extension = Path.GetExtension(file.FileName);
        
        return !extensions.Contains(extension.ToLower()) 
            ? new ValidationResult(GetErrorMessage()) 
            : ValidationResult.Success;
    }

    private string GetErrorMessage()
    {
        return $"Allowed file extensions are {string.Join(", ", extensions)}.";
    }
}