using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class IngredientsController(AppDbContext context) : Controller
{
    // GET: Ingredients
    public async Task<IActionResult> Index()
    {
        return View(await context.Ingredients.ToListAsync());
    }

    // GET: Ingredients/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ingredient = await context.Ingredients
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ingredient == null)
        {
            return NotFound();
        }

        return View(ingredient);
    }

    // GET: Ingredients/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Ingredients/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Id")] Ingredient ingredient)
    {
        if (ModelState.IsValid)
        {
            ingredient.Id = Guid.NewGuid();
            context.Add(ingredient);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(ingredient);
    }

    // GET: Ingredients/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ingredient = await context.Ingredients.FindAsync(id);
        if (ingredient == null)
        {
            return NotFound();
        }
        return View(ingredient);
    }

    // POST: Ingredients/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] Ingredient ingredient)
    {
        if (id != ingredient.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(ingredient);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(ingredient.Id))
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
        return View(ingredient);
    }

    // GET: Ingredients/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ingredient = await context.Ingredients
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ingredient == null)
        {
            return NotFound();
        }

        return View(ingredient);
    }

    // POST: Ingredients/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var ingredient = await context.Ingredients.FindAsync(id);
        if (ingredient != null)
        {
            context.Ingredients.Remove(ingredient);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool IngredientExists(Guid id)
    {
        return context.Ingredients.Any(e => e.Id == id);
    }
}