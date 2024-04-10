using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
public class UnitController : Controller
{
    private readonly AppDbContext _context;

    public UnitController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Unit
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.Units.Include(u => u.IngredientType);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Unit/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var unit = await _context.Units
            .Include(u => u.IngredientType)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (unit == null)
        {
            return NotFound();
        }

        return View(unit);
    }

    // GET: Unit/Create
    public IActionResult Create()
    {
        ViewData["IngredientTypeId"] = new SelectList(_context.IngredientTypes, "Id", "Description");
        return View();
    }

    // POST: Unit/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IngredientTypeId,Name,Abbreviation,UnitMultiplier,Id")] Unit unit)
    {
        if (ModelState.IsValid)
        {
            unit.Id = Guid.NewGuid();
            _context.Add(unit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["IngredientTypeId"] = new SelectList(_context.IngredientTypes, "Id", "Description", unit.IngredientTypeId);
        return View(unit);
    }

    // GET: Unit/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var unit = await _context.Units.FindAsync(id);
        if (unit == null)
        {
            return NotFound();
        }
        ViewData["IngredientTypeId"] = new SelectList(_context.IngredientTypes, "Id", "Description", unit.IngredientTypeId);
        return View(unit);
    }

    // POST: Unit/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("IngredientTypeId,Name,Abbreviation,UnitMultiplier,Id")] Unit unit)
    {
        if (id != unit.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(unit);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitExists(unit.Id))
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
        ViewData["IngredientTypeId"] = new SelectList(_context.IngredientTypes, "Id", "Description", unit.IngredientTypeId);
        return View(unit);
    }

    // GET: Unit/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var unit = await _context.Units
            .Include(u => u.IngredientType)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (unit == null)
        {
            return NotFound();
        }

        return View(unit);
    }

    // POST: Unit/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var unit = await _context.Units.FindAsync(id);
        if (unit != null)
        {
            _context.Units.Remove(unit);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UnitExists(Guid id)
    {
        return _context.Units.Any(e => e.Id == id);
    }
}