using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
public class RecipeController : Controller
{
    private readonly AppDbContext _context;

    public RecipeController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Recipe
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.Recipes.Include(r => r.AuthorUser).Include(r => r.UpdatingUser);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Recipe/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _context.Recipes
            .Include(r => r.AuthorUser)
            .Include(r => r.UpdatingUser)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // GET: Recipe/Create
    public IActionResult Create()
    {
        ViewData["AuthorUserId"] = new SelectList(_context.Users, "Id", "FirstName");
        ViewData["UpdatingUserId"] = new SelectList(_context.Users, "Id", "FirstName");
        return View();
    }

    // POST: Recipe/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Description,Image,Instructions,PrepationTime,CookingTime,Servings,IsVegetarian,IsVegan,IsGlutenFree,CreatedAt,AuthorUserId,UpdatedAt,UpdatingUserId,Id")] Recipe recipe)
    {
        if (ModelState.IsValid)
        {
            recipe.Id = Guid.NewGuid();
            _context.Add(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["AuthorUserId"] = new SelectList(_context.Users, "Id", "FirstName", recipe.AuthorUserId);
        ViewData["UpdatingUserId"] = new SelectList(_context.Users, "Id", "FirstName", recipe.UpdatingUserId);
        return View(recipe);
    }

    // GET: Recipe/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }
        ViewData["AuthorUserId"] = new SelectList(_context.Users, "Id", "FirstName", recipe.AuthorUserId);
        ViewData["UpdatingUserId"] = new SelectList(_context.Users, "Id", "FirstName", recipe.UpdatingUserId);
        return View(recipe);
    }

    // POST: Recipe/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Title,Description,Image,Instructions,PrepationTime,CookingTime,Servings,IsVegetarian,IsVegan,IsGlutenFree,CreatedAt,AuthorUserId,UpdatedAt,UpdatingUserId,Id")] Recipe recipe)
    {
        if (id != recipe.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(recipe);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(recipe.Id))
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
        ViewData["AuthorUserId"] = new SelectList(_context.Users, "Id", "FirstName", recipe.AuthorUserId);
        ViewData["UpdatingUserId"] = new SelectList(_context.Users, "Id", "FirstName", recipe.UpdatingUserId);
        return View(recipe);
    }

    // GET: Recipe/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _context.Recipes
            .Include(r => r.AuthorUser)
            .Include(r => r.UpdatingUser)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // POST: Recipe/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe != null)
        {
            _context.Recipes.Remove(recipe);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RecipeExists(Guid id)
    {
        return _context.Recipes.Any(e => e.Id == id);
    }
}