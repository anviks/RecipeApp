using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
public class ReviewController : Controller
{
    private readonly AppDbContext _context;

    public ReviewController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Review
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.Reviews.Include(r => r.Recipe).Include(r => r.User);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Review/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var review = await _context.Reviews
            .Include(r => r.Recipe)
            .Include(r => r.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (review == null)
        {
            return NotFound();
        }

        return View(review);
    }

    // GET: Review/Create
    public IActionResult Create()
    {
        ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
        return View();
    }

    // POST: Review/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("UserId,RecipeId,Rating,Content,CreatedAt,Id")] Review review)
    {
        if (ModelState.IsValid)
        {
            review.Id = Guid.NewGuid();
            _context.Add(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description", review.RecipeId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", review.UserId);
        return View(review);
    }

    // GET: Review/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound();
        }
        ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description", review.RecipeId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", review.UserId);
        return View(review);
    }

    // POST: Review/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("UserId,RecipeId,Rating,Content,CreatedAt,Id")] Review review)
    {
        if (id != review.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(review);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(review.Id))
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
        ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description", review.RecipeId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", review.UserId);
        return View(review);
    }

    // GET: Review/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var review = await _context.Reviews
            .Include(r => r.Recipe)
            .Include(r => r.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (review == null)
        {
            return NotFound();
        }

        return View(review);
    }

    // POST: Review/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review != null)
        {
            _context.Reviews.Remove(review);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ReviewExists(Guid id)
    {
        return _context.Reviews.Any(e => e.Id == id);
    }
}