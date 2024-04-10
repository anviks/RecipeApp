using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
public class IngredientTypeController : Controller
{
    private readonly AppDbContext _context;

    public IngredientTypeController(AppDbContext context)
    {
        _context = context;
    }

    // GET: IngredientType
    public async Task<IActionResult> Index()
    {
        return View(await _context.IngredientTypes.ToListAsync());
    }

    // GET: IngredientType/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ingredientType = await _context.IngredientTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ingredientType == null)
        {
            return NotFound();
        }

        return View(ingredientType);
    }

    // GET: IngredientType/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: IngredientType/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,Id")] IngredientType ingredientType)
    {
        if (ModelState.IsValid)
        {
            ingredientType.Id = Guid.NewGuid();
            _context.Add(ingredientType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(ingredientType);
    }

    // GET: IngredientType/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ingredientType = await _context.IngredientTypes.FindAsync(id);
        if (ingredientType == null)
        {
            return NotFound();
        }
        return View(ingredientType);
    }

    // POST: IngredientType/Edit/5
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
                _context.Update(ingredientType);
                await _context.SaveChangesAsync();
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

    // GET: IngredientType/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ingredientType = await _context.IngredientTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ingredientType == null)
        {
            return NotFound();
        }

        return View(ingredientType);
    }

    // POST: IngredientType/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var ingredientType = await _context.IngredientTypes.FindAsync(id);
        if (ingredientType != null)
        {
            _context.IngredientTypes.Remove(ingredientType);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool IngredientTypeExists(Guid id)
    {
        return _context.IngredientTypes.Any(e => e.Id == id);
    }
}