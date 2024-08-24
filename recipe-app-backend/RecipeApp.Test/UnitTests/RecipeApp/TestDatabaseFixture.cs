using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RecipeApp.Infrastructure;
using RecipeApp.Infrastructure.Data.EntityFramework;
using RecipeApp.Infrastructure.Data.EntityFramework.Entities.Identity;

namespace RecipeApp.Test.UnitTests.RecipeApp;

public class TestDatabaseFixture
{
    internal const string WebRootPath = "";
    internal const string Email = "test-user@gmail.com";
    internal const string Username = "test.user";
    internal const string Password = "Test123!";
    internal static readonly Guid UserId = new("00000000-0000-0000-0001-000000000000");
    internal readonly IMapper Mapper;
    private static readonly object Lock = new();
    private static bool _databaseInitialized;

    public TestDatabaseFixture()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
            cfg.AddProfile<global::RecipeApp.Application.AutoMapperProfile>();
        });
        Mapper = config.CreateMapper();
        
        lock (Lock)
        {
            if (_databaseInitialized) return;

            using (AppDbContext context = CreateContext())
            {
                NpgsqlConnection.GlobalTypeMapper.EnableDynamicJson();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                SeedData(context);

                context.SaveChanges();
            }

            _databaseInitialized = true;
        }
    }

    private static void SeedData(AppDbContext context)
    {
        context.Users.Add(new AppUser
        {
            Id = UserId,
            UserName = Username,
            Email = Email,
        });
        
        context.SaveChanges();
    }

    public AppDbContext CreateContext()
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables();
        IConfiguration configuration = configurationBuilder.Build();
        
        var connectionString = configuration.GetConnectionString("TestDbConnection");
        
        return new AppDbContext(
            new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(connectionString)
                .Options);
    }
}