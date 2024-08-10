using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RecipeIngredientsController(AppDbContext context) : Controller
{
    // GET: RecipeIngredients
    public async Task<IActionResult> Index()
    {
        var appDbContext = context.RecipeIngredients.Include(r => r.Ingredient).Include(r => r.Recipe).Include(r => r.Unit);
        return View(await appDbContext.ToListAsync());
    }

    // GET: RecipeIngredients/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipeIngredient = await context.RecipeIngredients
            .Include(r => r.Ingredient)
            .Include(r => r.Recipe)
            .Include(r => r.Unit)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (recipeIngredient == null)
        {
            return NotFound();
        }

        return View(recipeIngredient);
    }

    // GET: RecipeIngredients/Create
    public IActionResult Create()
    {
        ViewData["IngredientId"] = new SelectList(context.Ingredients, "Id", "Id");
        ViewData["RecipeId"] = new SelectList(context.Recipes, "Id", "Description");
        ViewData["UnitId"] = new SelectList(context.Units, "Id", "Id");
        return View();
    }

    // POST: RecipeIngredients/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CustomUnit,Quantity,IngredientModifier,UnitId,RecipeId,IngredientId,Id")] RecipeIngredient recipeIngredient)
    {
        if (ModelState.IsValid)
        {
            recipeIngredient.Id = Guid.NewGuid();
            context.Add(recipeIngredient);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["IngredientId"] = new SelectList(context.Ingredients, "Id", "Id", recipeIngredient.IngredientId);
        ViewData["RecipeId"] = new SelectList(context.Recipes, "Id", "Description", recipeIngredient.RecipeId);
        ViewData["UnitId"] = new SelectList(context.Units, "Id", "Id", recipeIngredient.UnitId);
        return View(recipeIngredient);
    }

    // GET: RecipeIngredients/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipeIngredient = await context.RecipeIngredients.FindAsync(id);
        if (recipeIngredient == null)
        {
            return NotFound();
        }
        ViewData["IngredientId"] = new SelectList(context.Ingredients, "Id", "Id", recipeIngredient.IngredientId);
        ViewData["RecipeId"] = new SelectList(context.Recipes, "Id", "Description", recipeIngredient.RecipeId);
        ViewData["UnitId"] = new SelectList(context.Units, "Id", "Id", recipeIngredient.UnitId);
        return View(recipeIngredient);
    }

    // POST: RecipeIngredients/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("CustomUnit,Quantity,IngredientModifier,UnitId,RecipeId,IngredientId,Id")] RecipeIngredient recipeIngredient)
    {
        if (id != recipeIngredient.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(recipeIngredient);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeIngredientExists(recipeIngredient.Id))
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
        ViewData["IngredientId"] = new SelectList(context.Ingredients, "Id", "Id", recipeIngredient.IngredientId);
        ViewData["RecipeId"] = new SelectList(context.Recipes, "Id", "Description", recipeIngredient.RecipeId);
        ViewData["UnitId"] = new SelectList(context.Units, "Id", "Id", recipeIngredient.UnitId);
        return View(recipeIngredient);
    }

    // GET: RecipeIngredients/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipeIngredient = await context.RecipeIngredients
            .Include(r => r.Ingredient)
            .Include(r => r.Recipe)
            .Include(r => r.Unit)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (recipeIngredient == null)
        {
            return NotFound();
        }

        return View(recipeIngredient);
    }

    // POST: RecipeIngredients/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var recipeIngredient = await context.RecipeIngredients.FindAsync(id);
        if (recipeIngredient != null)
        {
            context.RecipeIngredients.Remove(recipeIngredient);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RecipeIngredientExists(Guid id)
    {
        return context.RecipeIngredients.Any(e => e.Id == id);
    }
}