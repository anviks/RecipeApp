using RecipeApp.Resources.Errors;

namespace RecipeApp.Application.Exceptions;

public class MissingImageException() : Exception(ValidationErrors.MissingImageFile);