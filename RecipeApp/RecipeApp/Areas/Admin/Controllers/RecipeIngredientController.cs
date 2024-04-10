using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
public class RecipeIngredientController : Controller
{
    private readonly AppDbContext _context;

    public RecipeIngredientController(AppDbContext context)
    {
        _context = context;
    }

    // GET: RecipeIngredient
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.RecipeIngredients.Include(r => r.Ingredient).Include(r => r.Recipe).Include(r => r.Unit);
        return View(await appDbContext.ToListAsync());
    }

    // GET: RecipeIngredient/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipeIngredient = await _context.RecipeIngredients
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

    // GET: RecipeIngredient/Create
    public IActionResult Create()
    {
        ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name");
        ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description");
        ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Name");
        return View();
    }

    // POST: RecipeIngredient/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("RecipeId,IngredientId,UnitId,CustomUnit,Quantity,IngredientModifier,Id")] RecipeIngredient recipeIngredient)
    {
        if (ModelState.IsValid)
        {
            recipeIngredient.Id = Guid.NewGuid();
            _context.Add(recipeIngredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", recipeIngredient.IngredientId);
        ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description", recipeIngredient.RecipeId);
        ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Name", recipeIngredient.UnitId);
        return View(recipeIngredient);
    }

    // GET: RecipeIngredient/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipeIngredient = await _context.RecipeIngredients.FindAsync(id);
        if (recipeIngredient == null)
        {
            return NotFound();
        }
        ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", recipeIngredient.IngredientId);
        ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description", recipeIngredient.RecipeId);
        ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Name", recipeIngredient.UnitId);
        return View(recipeIngredient);
    }

    // POST: RecipeIngredient/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("RecipeId,IngredientId,UnitId,CustomUnit,Quantity,IngredientModifier,Id")] RecipeIngredient recipeIngredient)
    {
        if (id != recipeIngredient.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(recipeIngredient);
                await _context.SaveChangesAsync();
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
        ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", recipeIngredient.IngredientId);
        ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description", recipeIngredient.RecipeId);
        ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Name", recipeIngredient.UnitId);
        return View(recipeIngredient);
    }

    // GET: RecipeIngredient/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipeIngredient = await _context.RecipeIngredients
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

    // POST: RecipeIngredient/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var recipeIngredient = await _context.RecipeIngredients.FindAsync(id);
        if (recipeIngredient != null)
        {
            _context.RecipeIngredients.Remove(recipeIngredient);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RecipeIngredientExists(Guid id)
    {
        return _context.RecipeIngredients.Any(e => e.Id == id);
    }
}