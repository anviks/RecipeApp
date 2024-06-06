using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp;

public static class DataSeeder
{
    public static async Task SeedAdminUser(this IApplicationBuilder applicationBuilder)
    {
        using IServiceScope serviceScope = applicationBuilder.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        await using var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        using var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        using var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
        IdentityResult res;

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            res = await roleManager.CreateAsync(new AppRole
            {
                Name = "Admin"
            });

            if (!res.Succeeded)
            {
                Console.WriteLine(res.ToString());
            }
        }

        if ((await userManager.GetUsersInRoleAsync("Admin")).Any()) return;

        var user = new AppUser
        {
            Email = "anviks@taltech.ee",
            UserName = "admin",
            SecurityStamp = Guid.NewGuid().ToString()
        };

        res = await userManager.CreateAsync(user, "asdasd");
        if (!res.Succeeded)
        {
            Console.WriteLine(res.ToString());
        }

        res = await userManager.AddToRoleAsync(user, "Admin");
        if (!res.Succeeded)
        {
            Console.WriteLine(res.ToString());
        }
    }

    public static async Task SeedSampleData(this IApplicationBuilder applicationBuilder)
    {
        using IServiceScope serviceScope = applicationBuilder.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        await using var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();

        await SeedSamples(context);
        
        await context.SaveChangesAsync();
    }

    private static async Task SeedSamples(AppDbContext context)
    {
        if (await context.Samples.AnyAsync()) return;
        
        var samples = new List<Sample>
        {
            new()
            {
                Field = "Sample 1"
            },
            new()
            {
                Field = "Sample 2"
            },
            new()
            {
                Field = "Sample 3"
            }
        };
        
        await context.Samples.AddRangeAsync(samples);
    }
}