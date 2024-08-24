using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Test.UnitTests.Base.Infrastructure;

public class TestDbContext(DbContextOptions options)
    : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<TestEntity> TestEntities { get; set; } = default!;
}
