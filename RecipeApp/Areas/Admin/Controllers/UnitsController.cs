using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UnitsController(AppDbContext context) : Controller
{
    // GET: Units
    public async Task<IActionResult> Index()
    {
        var appDbContext = context.Units.Include(u => u.IngredientType);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Units/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var unit = await context.Units
            .Include(u => u.IngredientType)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (unit == null)
        {
            return NotFound();
        }

        return View(unit);
    }

    // GET: Units/Create
    public IActionResult Create()
    {
        ViewData["IngredientTypeId"] = new SelectList(context.IngredientTypes, "Id", "Id");
        return View();
    }

    // POST: Units/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Abbreviation,UnitMultiplier,IngredientTypeId,Id")] Unit unit)
    {
        if (ModelState.IsValid)
        {
            unit.Id = Guid.NewGuid();
            context.Add(unit);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["IngredientTypeId"] = new SelectList(context.IngredientTypes, "Id", "Id", unit.IngredientTypeId);
        return View(unit);
    }

    // GET: Units/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var unit = await context.Units.FindAsync(id);
        if (unit == null)
        {
            return NotFound();
        }
        ViewData["IngredientTypeId"] = new SelectList(context.IngredientTypes, "Id", "Id", unit.IngredientTypeId);
        return View(unit);
    }

    // POST: Units/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Name,Abbreviation,UnitMultiplier,IngredientTypeId,Id")] Unit unit)
    {
        if (id != unit.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(unit);
                await context.SaveChangesAsync();
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
        ViewData["IngredientTypeId"] = new SelectList(context.IngredientTypes, "Id", "Id", unit.IngredientTypeId);
        return View(unit);
    }

    // GET: Units/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var unit = await context.Units
            .Include(u => u.IngredientType)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (unit == null)
        {
            return NotFound();
        }

        return View(unit);
    }

    // POST: Units/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var unit = await context.Units.FindAsync(id);
        if (unit != null)
        {
            context.Units.Remove(unit);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UnitExists(Guid id)
    {
        return context.Units.Any(e => e.Id == id);
    }
}