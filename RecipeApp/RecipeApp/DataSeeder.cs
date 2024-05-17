using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp;

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

    public static async Task SeedSampleData(IApplicationBuilder applicationBuilder)
    {
        using IServiceScope serviceScope = applicationBuilder.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        await using var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();

        if (await context.Categories.AnyAsync()) return;

        var categories = new[]
        {
            new Category
            {
                Name = "Breakfast",
                Description = "Morning meal"
            },
            new Category
            {
                Name = "Lunch",
                Description = "Midday meal"
            },
            new Category
            {
                Name = "Dinner",
                Description = "Evening meal"
            }
        };

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();

        var ingredients = new[]
        {
            new Ingredient
            {
                Name = "Egg"
            },
            new Ingredient
            {
                Name = "Bacon"
            },
            new Ingredient
            {
                Name = "Bread"
            },
            new Ingredient
            {
                Name = "Butter"
            },
            new Ingredient
            {
                Name = "Cheese"
            },
            new Ingredient
            {
                Name = "Tomato"
            },
            new Ingredient
            {
                Name = "Cucumber"
            },
            new Ingredient
            {
                Name = "Salad"
            },
            new Ingredient
            {
                Name = "Pasta"
            },
            new Ingredient
            {
                Name = "Minced meat"
            },
            new Ingredient
            {
                Name = "Potato"
            },
            new Ingredient
            {
                Name = "Carrot"
            },
            new Ingredient
            {
                Name = "Onion"
            },
            new Ingredient
            {
                Name = "Garlic"
            },
            new Ingredient
            {
                Name = "Milk"
            },
            new Ingredient
            {
                Name = "Flour"
            },
            new Ingredient
            {
                Name = "Sugar"
            },
            new Ingredient
            {
                Name = "Salt"
            },
            new Ingredient
            {
                Name = "Pepper"
            },
            new Ingredient
            {
                Name = "Oil"
            },
            new Ingredient
            {
                Name = "Vinegar"
            }
        };
        
        await context.Ingredients.AddRangeAsync(ingredients);
        await context.SaveChangesAsync();
    }
}