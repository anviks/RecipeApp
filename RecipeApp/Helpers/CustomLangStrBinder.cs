using Base.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RecipeApp.Helpers;

public class CustomLangStrBinder(ILoggerFactory? loggerFactory) : IModelBinder
{
    private readonly ILogger<CustomLangStrBinder>? _logger = loggerFactory?.CreateLogger<CustomLangStrBinder>();

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);
        ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;

        if (value == null)
        {
            return Task.CompletedTask;
        }

        _logger?.LogDebug("LangStrBinder: {}", value);
        bindingContext.Result = ModelBindingResult.Success(new LangStr(value));

        return Task.CompletedTask;
    }
}
