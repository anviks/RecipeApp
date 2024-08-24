using System.ComponentModel.DataAnnotations;
using RecipeApp.Base.Infrastructure.Data;

namespace RecipeApp.Test.UnitTests.Base;

public class TestEntity : BaseEntityId //, IDomainAppUser<IdentityUser<Guid>>, IDomainAppUserId<Guid>
{
    [MaxLength(128)]
    public string Value { get; set; } = default!;
    //public Guid AppUserId { get; set; }
    //public IdentityUser<Guid>? AppUser { get; set; }
}
