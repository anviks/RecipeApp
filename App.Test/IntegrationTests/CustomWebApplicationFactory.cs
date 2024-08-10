using App.DAL.EF;
using App.Domain.Identity;
using Base.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App.Test.IntegrationTests;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup: class
{
    private const string Email = "test-user@gmail.com";
    private const string Username = "test.user";
    private const string Password = "Test123!";
    private static readonly Guid UserId = Guid.Parse("00000000-0000-0000-0000-000000001000");
    public string GetEmail => Email;
    public string GetUsername => Username;
    public string GetPassword => Password;
    public Guid GetUserId => UserId;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
        });
        
        builder.ConfigureServices((context, services) =>
        {
            // find DbContext
            ServiceDescriptor? descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<AppDbContext>));

            // if found - remove
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // and new DbContext
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = context.Configuration.GetConnectionString("TestDbConnection");
                options.UseNpgsql(connectionString);
            });
            
            // create db and seed data
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            using IServiceScope scope = serviceProvider.CreateScope();
            IServiceProvider scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
            var userManager = scopedServices.GetRequiredService<UserManager<AppUser>>();
            db.Database.EnsureDeleted();
            db.Database.Migrate();
            
            SeedData(db, userManager);
        });
    }
    
    private static void SeedData(AppDbContext context, UserManager<AppUser> userManager)
    {
        userManager.CreateAsync(new AppUser
        {
            Id = UserId,
            UserName = Username,
            Email = Email
        }, Password).Wait();
        
        context.Categories.AddRange(
            new Domain.Category
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Name = new LangStr("Category 1", "en-GB"),
                Description = new LangStr("Category description 1", "en-GB")
            },
            new Domain.Category
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Name = new LangStr("Category 2", "en-GB"),
                Description = new LangStr("Category description 2", "en-GB")
            }
        );
        
        context.Ingredients.AddRange(
            new Domain.Ingredient
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000100"),
                Name = new LangStr("Ingredient 1", "en-GB")
            },
            new Domain.Ingredient
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000101"),
                Name = new LangStr("Ingredient 2", "en-GB")
            }
        );
        
        context.IngredientTypes.AddRange(
            new Domain.IngredientType
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000200"),
                Name = new LangStr("IngredientType 1", "en-GB"),
                Description = new LangStr("IngredientType description 1", "en-GB")
            },
            new Domain.IngredientType
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000201"),
                Name = new LangStr("IngredientType 2", "en-GB"),
                Description = new LangStr("IngredientType description 2", "en-GB")
            }
        );
        
        // context.IngredientTypeAssociations.AddRange();
        
        context.SaveChanges();
    }
}