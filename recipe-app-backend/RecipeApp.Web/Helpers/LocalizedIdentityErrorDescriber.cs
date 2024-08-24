using Microsoft.AspNetCore.Identity;
using RecipeApp.Resources.Errors;

namespace RecipeApp.Web.Helpers;

public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() => new()
    {
        Code = nameof(DefaultError),
        Description = IdentityErrors.DefaultError
    };
}
