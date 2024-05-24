using App.DAL.EF;
using App.Domain.Identity;
using AutoMapper;
using Base.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace App.Test;

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
            cfg.AddProfile<DAL.EF.AutoMapperProfile>();
            cfg.AddProfile<BLL.AutoMapperProfile>();
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

    private void SeedData(AppDbContext context)
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
            .AddJsonFile("appsettings.Testing.json");
        IConfiguration configuration = configurationBuilder.Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        return new AppDbContext(
            new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(connectionString)
                .Options);
    }
}