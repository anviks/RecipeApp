using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RecipeApp.Resources.Errors;

namespace RecipeApp.Web.Helpers;

public class ConfigureModelBindingLocalization : IConfigureOptions<MvcOptions>
{
    public void Configure(MvcOptions options)
    {
        options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => 
            string.Format(ModelBindingErrors.ValueIsInvalid, x));
        options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x =>
            string.Format(ModelBindingErrors.ValueMustBeANumber, x));
        options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x =>
            string.Format(ModelBindingErrors.MissingBindRequiredValue, x));
        options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) =>
            string.Format(ModelBindingErrors.AttemptedValueIsInvalid, x, y));
        options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() =>
            ModelBindingErrors.MissingKeyOrValue);
        options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() =>
            ModelBindingErrors.MissingRequestBodyRequiredValue);
        options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() =>
            ModelBindingErrors.NonPropertyUnknownValueIsInvalid);
        options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() =>
            ModelBindingErrors.NonPropertyValueMustBeANumber);
        options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(x =>
            string.Format(ModelBindingErrors.UnknownValueIsInvalid, x));
        options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x =>
            string.Format(ModelBindingErrors.ValueMustNotBeNull, x));
        options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x =>
            string.Format(ModelBindingErrors.NonPropertyAttemptedValueIsInvalid, x));
        
        // https://stackoverflow.com/questions/40828570/asp-net-core-model-binding-error-messages-localization
        
        // SetNonPropertyUnknownValueIsInvalidAccessor
        // localizer["The supplied value is invalid."]);
    }
}
