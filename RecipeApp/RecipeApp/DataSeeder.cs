using App.DAL.EF;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp;

public static class DataSeeder
{
    public static async Task SeedAdminUser(IApplicationBuilder applicationBuilder)
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
            res = await roleManager.CreateAsync(new AppRole()
            {
                Name = "Admin"
            });

            if (!res.Succeeded)
            {
                Console.WriteLine(res.ToString());
            }
        }
        
        if ((await userManager.GetUsersInRoleAsync("Admin")).Any()) return;

        var user = new AppUser()
        {
            Email = "admin@eesti.ee",
            UserName = "tere",
            FirstName = "Andreas",
            LastName = "Viks",
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        res = await userManager.CreateAsync(user, "123.PUHKAv");
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
}