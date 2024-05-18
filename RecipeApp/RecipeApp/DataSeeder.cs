using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using Base.Domain;
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

    public static async Task SeedSampleData(this IApplicationBuilder applicationBuilder)
    {
        using IServiceScope serviceScope = applicationBuilder.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        await using var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();

        await SeedCategories(context);
        await SeedIngredients(context);
        await SeedIngredientTypesAndUnits(context);
        
        await context.SaveChangesAsync();
    }

    private static async Task SeedCategories(AppDbContext context)
    {
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
    }

    private static async Task SeedIngredients(AppDbContext context)
    {
        if (await context.Ingredients.AnyAsync()) return;

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
    }

    private static async Task SeedIngredientTypesAndUnits(AppDbContext context)
    {
        var volumetricId = Guid.NewGuid();
        var weighableId = Guid.NewGuid();
        var countableId = Guid.NewGuid();

        var ingredientTypesExist = await context.IngredientTypes.AnyAsync();
        var unitsExist = await context.Units.AnyAsync();
        
        switch (ingredientTypesExist)
        {
            case true when unitsExist:
                return;
            case true when !unitsExist:
                context.IngredientTypes.RemoveRange(context.IngredientTypes);
                break;
            case false when unitsExist:
                context.Units.RemoveRange(context.Units);
                break;
        }

        await SeedIngredientTypes(context, volumetricId, weighableId, countableId);
        await SeedUnits(context, volumetricId, weighableId, countableId);
    }

    private static async Task SeedIngredientTypes(AppDbContext context, Guid volumetricId, Guid weighableId, Guid countableId)
    {
        var ingredientTypes = new[]
        {
            new IngredientType
            {
                Id = volumetricId,
                Name = new LangStr
                {
                    ["en"] = "Volumetric",
                    ["et"] = "Mahuline",
                    ["cs"] = "Objemový"
                },
                Description = new LangStr
                {
                    ["en"] = "An ingredient that can be measured by volume.",
                    ["et"] = "Koostisosa, mida saab mõõta mahu järgi.",
                    ["cs"] = "Složka, kterou lze měřit podle objemu."
                }
            },
            new IngredientType
            {
                Id = weighableId,
                Name = new LangStr
                {
                    ["en"] = "Weighable",
                    ["et"] = "Kaalutav",
                    ["cs"] = "Vážitelný"
                },
                Description = new LangStr
                {
                    ["en"] = "An ingredient that can be measured by weight.",
                    ["et"] = "Koostisosa, mida saab mõõta kaalu järgi.",
                    ["cs"] = "Složka, kterou lze měřit podle hmotnosti."
                }
            },
            new IngredientType
            {
                Id = countableId,
                Name = new LangStr
                {
                    ["en"] = "Countable",
                    ["et"] = "Loendatav",
                    ["cs"] = "Počitatelný"
                },
                Description = new LangStr
                {
                    ["en"] = "An ingredient that can be counted.",
                    ["et"] = "Koostisosa, mida saab loendada.",
                    ["cs"] = "Složka, kterou lze počítat."
                }
            }
        };
        
        await context.IngredientTypes.AddRangeAsync(ingredientTypes);
    }

    private static async Task SeedUnits(AppDbContext context, Guid volumetricId, Guid weighableId, Guid countableId)
    { 
        var units = new[]
        {
            new Unit
            {
                Id = Guid.NewGuid(),
                Name = new LangStr
                {
                    ["en"] = "gram",
                    ["et"] = "gramm",
                    ["cs"] = "gram"
                },
                Abbreviation = "g",
                IngredientTypeId = weighableId,
                UnitMultiplier = 1
            },
            new Unit
            {
                Id = Guid.NewGuid(),
                Name = new LangStr
                {
                    ["en"] = "kilogram",
                    ["et"] = "kilogramm",
                    ["cs"] = "kilogram"
                },
                Abbreviation = "kg",
                IngredientTypeId = weighableId,
                UnitMultiplier = 1000
            },
            new Unit
            {
                Id = Guid.NewGuid(),
                Name = new LangStr
                {
                    ["en"] = "ounce",
                    ["et"] = "unts",
                    ["cs"] = "unce"
                },
                Abbreviation = "oz",
                IngredientTypeId = weighableId,
                UnitMultiplier = 28.3495f
            },
            new Unit
            {
                Id = Guid.NewGuid(),
                Name = new LangStr
                {
                    ["en"] = "pound",
                    ["et"] = "nael",
                    ["cs"] = "libra"
                },
                Abbreviation = "lb",
                IngredientTypeId = weighableId,
                UnitMultiplier = 453.592f
            },
            new Unit
            {
                Id = Guid.NewGuid(),
                Name = new LangStr
                {
                    ["en"] = "milliliter",
                    ["et"] = "milliliiter",
                    ["cs"] = "mililitr"
                },
                Abbreviation = "ml",
                IngredientTypeId = volumetricId,
                UnitMultiplier = 1
            },
            new Unit
            {
                Id = Guid.NewGuid(),
                Name = new LangStr
                {
                    ["en"] = "liter",
                    ["et"] = "liiter",
                    ["cs"] = "litr"
                },
                Abbreviation = "l",
                IngredientTypeId = volumetricId,
                UnitMultiplier = 1000
            },
            new Unit
            {
                Id = Guid.NewGuid(),
                Name = new LangStr
                {
                    ["en"] = "fluid ounce",
                    ["et"] = "vedelikuunts",
                    ["cs"] = "tekutá unce"
                },
                Abbreviation = "fl oz",
                IngredientTypeId = volumetricId,
                UnitMultiplier = 29.5735f
            },
            new Unit
            {
                Id = Guid.NewGuid(),
                Name = new LangStr
                {
                    ["en"] = "cup",
                    ["et"] = "tass",
                    ["cs"] = "šálek"
                },
                Abbreviation = "c",
                IngredientTypeId = volumetricId,
                UnitMultiplier = 236.588f
            },
            new Unit
            {
                Id = Guid.NewGuid(),
                Name = new LangStr
                {
                    ["en"] = "pint",
                    ["et"] = "pint",
                    ["cs"] = "pinta"
                },
                Abbreviation = "pt",
                IngredientTypeId = volumetricId,
                UnitMultiplier = 473.176f
            },
            new Unit
            {
                Id = Guid.NewGuid(),
                Name = new LangStr
                {
                    ["en"] = "quart",
                    ["et"] = "kvart",
                    ["cs"] = "kvart"
                },
                Abbreviation = "qt",
                IngredientTypeId = volumetricId,
                UnitMultiplier = 946.353f
            },
            new Unit
            {
                Id = Guid.NewGuid(),
                Name = new LangStr
                {
                    ["en"] = "gallon",
                    ["et"] = "gallon",
                    ["cs"] = "galon"
                },
                Abbreviation = "gal",
                IngredientTypeId = volumetricId,
                UnitMultiplier = 3785.41f
            },
            new Unit
            {
                Id = Guid.NewGuid(),
                Name = new LangStr
                {
                    ["en"] = "teaspoon",
                    ["et"] = "teelusikas",
                    ["cs"] = "čajová lžička"
                },
                Abbreviation = "tsp",
                IngredientTypeId = volumetricId,
                UnitMultiplier = 4.92892f
            },
            new Unit
            {
                Id = Guid.NewGuid(),
                Name = new LangStr
                {
                    ["en"] = "tablespoon",
                    ["et"] = "supilusikas",
                    ["cs"] = "polévková lžíce"
                },
                Abbreviation = "tbsp",
                IngredientTypeId = volumetricId,
                UnitMultiplier = 14.7868f
            }
        };
        
        await context.Units.AddRangeAsync(units);
    }
}