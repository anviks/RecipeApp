using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
public class RecipeCategoryController : Controller
{
    private readonly AppDbContext _context;

    public RecipeCategoryController(AppDbContext context)
    {
        _context = context;
    }

    // GET: RecipeCategory
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.RecipeCategories.Include(r => r.Category).Include(r => r.Recipe);
        return View(await appDbContext.ToListAsync());
    }

    // GET: RecipeCategory/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipeCategory = await _context.RecipeCategories
            .Include(r => r.Category)
            .Include(r => r.Recipe)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (recipeCategory == null)
        {
            return NotFound();
        }

        return View(recipeCategory);
    }

    // GET: RecipeCategory/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
        ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description");
        return View();
    }

    // POST: RecipeCategory/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CategoryId,RecipeId,Id")] RecipeCategory recipeCategory)
    {
        if (ModelState.IsValid)
        {
            recipeCategory.Id = Guid.NewGuid();
            _context.Add(recipeCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", recipeCategory.CategoryId);
        ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description", recipeCategory.RecipeId);
        return View(recipeCategory);
    }

    // GET: RecipeCategory/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipeCategory = await _context.RecipeCategories.FindAsync(id);
        if (recipeCategory == null)
        {
            return NotFound();
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", recipeCategory.CategoryId);
        ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description", recipeCategory.RecipeId);
        return View(recipeCategory);
    }

    // POST: RecipeCategory/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,RecipeId,Id")] RecipeCategory recipeCategory)
    {
        if (id != recipeCategory.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(recipeCategory);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeCategoryExists(recipeCategory.Id))
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
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", recipeCategory.CategoryId);
        ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description", recipeCategory.RecipeId);
        return View(recipeCategory);
    }

    // GET: RecipeCategory/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipeCategory = await _context.RecipeCategories
            .Include(r => r.Category)
            .Include(r => r.Recipe)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (recipeCategory == null)
        {
            return NotFound();
        }

        return View(recipeCategory);
    }

    // POST: RecipeCategory/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var recipeCategory = await _context.RecipeCategories.FindAsync(id);
        if (recipeCategory != null)
        {
            _context.RecipeCategories.Remove(recipeCategory);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RecipeCategoryExists(Guid id)
    {
        return _context.RecipeCategories.Any(e => e.Id == id);
    }
}