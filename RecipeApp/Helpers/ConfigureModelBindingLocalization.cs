using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace RecipeApp.Helpers;

public class ConfigureModelBindingLocalization : IConfigureOptions<MvcOptions>
{
    public void Configure(MvcOptions options)
    {
        options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => 
            string.Format(Base.Resources.ModelBindingErrors.ValueIsInvalid, x));
        options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x =>
            string.Format(Base.Resources.ModelBindingErrors.ValueMustBeANumber, x));
        options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x =>
            string.Format(Base.Resources.ModelBindingErrors.MissingBindRequiredValue, x));
        options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) =>
            string.Format(Base.Resources.ModelBindingErrors.AttemptedValueIsInvalid, x, y));
        options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() =>
            Base.Resources.ModelBindingErrors.MissingKeyOrValue);
        options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() =>
            Base.Resources.ModelBindingErrors.MissingRequestBodyRequiredValue);
        options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() =>
            Base.Resources.ModelBindingErrors.NonPropertyUnknownValueIsInvalid);
        options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() =>
            Base.Resources.ModelBindingErrors.NonPropertyValueMustBeANumber);
        options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(x =>
            string.Format(Base.Resources.ModelBindingErrors.UnknownValueIsInvalid, x));
        options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x =>
            string.Format(Base.Resources.ModelBindingErrors.ValueMustNotBeNull, x));
        options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x =>
            string.Format(Base.Resources.ModelBindingErrors.NonPropertyAttemptedValueIsInvalid, x));
        
        // https://stackoverflow.com/questions/40828570/asp-net-core-model-binding-error-messages-localization
        
        // SetNonPropertyUnknownValueIsInvalidAccessor
        // localizer["The supplied value is invalid."]);
    }
}
