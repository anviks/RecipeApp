using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
public class IngredientTypeAssociationController : Controller
{
    private readonly AppDbContext _context;

    public IngredientTypeAssociationController(AppDbContext context)
    {
        _context = context;
    }

    // GET: IngredientTypeAssociation
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.IngredientTypeAssociations.Include(i => i.Ingredient).Include(i => i.IngredientType);
        return View(await appDbContext.ToListAsync());
    }

    // GET: IngredientTypeAssociation/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ingredientTypeAssociation = await _context.IngredientTypeAssociations
            .Include(i => i.Ingredient)
            .Include(i => i.IngredientType)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ingredientTypeAssociation == null)
        {
            return NotFound();
        }

        return View(ingredientTypeAssociation);
    }

    // GET: IngredientTypeAssociation/Create
    public IActionResult Create()
    {
        ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name");
        ViewData["IngredientTypeId"] = new SelectList(_context.IngredientTypes, "Id", "Description");
        return View();
    }

    // POST: IngredientTypeAssociation/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IngredientId,IngredientTypeId,Id")] IngredientTypeAssociation ingredientTypeAssociation)
    {
        if (ModelState.IsValid)
        {
            ingredientTypeAssociation.Id = Guid.NewGuid();
            _context.Add(ingredientTypeAssociation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", ingredientTypeAssociation.IngredientId);
        ViewData["IngredientTypeId"] = new SelectList(_context.IngredientTypes, "Id", "Description", ingredientTypeAssociation.IngredientTypeId);
        return View(ingredientTypeAssociation);
    }

    // GET: IngredientTypeAssociation/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ingredientTypeAssociation = await _context.IngredientTypeAssociations.FindAsync(id);
        if (ingredientTypeAssociation == null)
        {
            return NotFound();
        }
        ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", ingredientTypeAssociation.IngredientId);
        ViewData["IngredientTypeId"] = new SelectList(_context.IngredientTypes, "Id", "Description", ingredientTypeAssociation.IngredientTypeId);
        return View(ingredientTypeAssociation);
    }

    // POST: IngredientTypeAssociation/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("IngredientId,IngredientTypeId,Id")] IngredientTypeAssociation ingredientTypeAssociation)
    {
        if (id != ingredientTypeAssociation.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(ingredientTypeAssociation);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientTypeAssociationExists(ingredientTypeAssociation.Id))
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
        ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", ingredientTypeAssociation.IngredientId);
        ViewData["IngredientTypeId"] = new SelectList(_context.IngredientTypes, "Id", "Description", ingredientTypeAssociation.IngredientTypeId);
        return View(ingredientTypeAssociation);
    }

    // GET: IngredientTypeAssociation/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ingredientTypeAssociation = await _context.IngredientTypeAssociations
            .Include(i => i.Ingredient)
            .Include(i => i.IngredientType)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ingredientTypeAssociation == null)
        {
            return NotFound();
        }

        return View(ingredientTypeAssociation);
    }

    // POST: IngredientTypeAssociation/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var ingredientTypeAssociation = await _context.IngredientTypeAssociations.FindAsync(id);
        if (ingredientTypeAssociation != null)
        {
            _context.IngredientTypeAssociations.Remove(ingredientTypeAssociation);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool IngredientTypeAssociationExists(Guid id)
    {
        return _context.IngredientTypeAssociations.Any(e => e.Id == id);
    }
}