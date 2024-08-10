using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Helpers.Validation.File;

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

    private string GetErrorMessage() => $"Allowed file extensions are {string.Join(", ", extensions)}.";
}