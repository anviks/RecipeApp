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

        SeedSamples(context);
        SeedCompanies(context);
        SeedActivityTypes(context);
        if (context.Companies.Any()) SeedRaffles(context);
        if (context.Raffles.Any()) SeedPrizes(context);
    }

    private static void SeedSamples(AppDbContext context)
    {
        if (context.Samples.Any()) return;

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

        context.Samples.AddRange(samples);
        context.SaveChanges();
    }

    private static void SeedCompanies(AppDbContext context)
    {
        if (context.Companies.Any()) return;

        var companies = new List<Company>
        {
            new()
            {
                CompanyName = "Bolt"
            },
            new()
            {
                CompanyName = "Uber"
            },
            new()
            {
                CompanyName = "Tallink"
            }
        };

        context.Companies.AddRange(companies);
        context.SaveChanges();
    }

    private static void SeedActivityTypes(AppDbContext context)
    {
        if (context.ActivityTypes.Any()) return;

        var activityTypes = new List<ActivityType>
        {
            new()
            {
                ActivityTypeName = "Running"
            },
            new()
            {
                ActivityTypeName = "Cycling"
            },
            new()
            {
                ActivityTypeName = "Swimming"
            },
            new()
            {
                ActivityTypeName = "Walking"
            }
        };

        context.ActivityTypes.AddRange(activityTypes);
        context.SaveChanges();
    }

    private static void SeedRaffles(AppDbContext context)
    {
        if (context.Raffles.Any()) return;

        var raffles = new List<Raffle>
        {
            new()
            {
                CompanyId = context.Companies.First().Id,
                RaffleName = "Raffle 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            },
            new()
            {
                CompanyId = context.Companies.First().Id,
                RaffleName = "Raffle 2",
                AllowAnonymousUsers = true,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(3)
            },
            new()
            {
                CompanyId = context.Companies.First().Id,
                RaffleName = "Raffle 3",
                VisibleToPublic = true,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5)
            }
        };

        context.Raffles.AddRange(raffles);
        context.SaveChanges();
    }

    private static void SeedPrizes(AppDbContext context)
    {
        if (context.Prizes.Any()) return;

        var prizes = new List<Prize>
        {
            new()
            {
                RaffleId = context.Raffles.First().Id,
                PrizeName = "Prize 1",
            },
            new()
            {
                RaffleId = context.Raffles.First().Id,
                PrizeName = "Prize 2",
            },
            new()
            {
                RaffleId = context.Raffles.First().Id,
                PrizeName = "Prize 3",
                // RaffleResultId = context.RaffleResults.First().Id
            }
        };

        context.Prizes.AddRange(prizes);
        context.SaveChanges();
    }
}