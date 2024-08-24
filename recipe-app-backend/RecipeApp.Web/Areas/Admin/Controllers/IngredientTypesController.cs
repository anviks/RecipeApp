using App.DAL.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Infrastructure.Data.EntityFramework;
using RecipeApp.Infrastructure.Data.EntityFramework.Entities;

namespace RecipeApp.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class IngredientTypesController(AppDbContext context) : Controller
{
    // GET: IngredientTypes
    public async Task<IActionResult> Index()
    {
        return View(await context.IngredientTypes.ToListAsync());
    }

    // GET: IngredientTypes/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ingredientType = await context.IngredientTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ingredientType == null)
        {
            return NotFound();
        }

        return View(ingredientType);
    }

    // GET: IngredientTypes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: IngredientTypes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,Id")] IngredientType ingredientType)
    {
        if (ModelState.IsValid)
        {
            ingredientType.Id = Guid.NewGuid();
            context.Add(ingredientType);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(ingredientType);
    }

    // GET: IngredientTypes/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ingredientType = await context.IngredientTypes.FindAsync(id);
        if (ingredientType == null)
        {
            return NotFound();
        }
        return View(ingredientType);
    }

    // POST: IngredientTypes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Id")] IngredientType ingredientType)
    {
        if (id != ingredientType.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(ingredientType);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientTypeExists(ingredientType.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(ingredientType);
    }

    // GET: IngredientTypes/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ingredientType = await context.IngredientTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ingredientType == null)
        {
            return NotFound();
        }

        return View(ingredientType);
    }

    // POST: IngredientTypes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var ingredientType = await context.IngredientTypes.FindAsync(id);
        if (ingredientType != null)
        {
            context.IngredientTypes.Remove(ingredientType);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool IngredientTypeExists(Guid id)
    {
        return context.IngredientTypes.Any(e => e.Id == id);
    }
}