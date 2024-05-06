using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace RecipeApp.Helpers;

public class ConfigureModelBindingLocalization : IConfigureOptions<MvcOptions>
{
    public void Configure(MvcOptions options)
    {
        options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => 
            string.Format(Base.Resources.ModelBindingErrors.ValueIsInvalid, x));
        
        // https://stackoverflow.com/questions/40828570/asp-net-core-model-binding-error-messages-localization
        
        // SetNonPropertyUnknownValueIsInvalidAccessor
        // localizer["The supplied value is invalid."]);
    }
}
