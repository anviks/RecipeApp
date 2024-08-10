using Microsoft.AspNetCore.Identity;

namespace RecipeApp.Helpers;

public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() => new()
    {
        Code = nameof(DefaultError),
        Description = Base.Resources.IdentityErrors.DefaultError
    };
}
