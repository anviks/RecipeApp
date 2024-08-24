using Microsoft.AspNetCore.Mvc.ModelBinding;
using RecipeApp.Base;

namespace RecipeApp.Web.Helpers;

public class CustomLangStrBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (context.Metadata.ModelType != typeof(LangStr)) return null;
        
        var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
        return new CustomLangStrBinder(loggerFactory);
    }
}
