using App.DAL.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Infrastructure.Data.EntityFramework;
using RecipeApp.Infrastructure.Data.EntityFramework.Entities;

namespace RecipeApp.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ReviewsController(AppDbContext context) : Controller
{
    // GET: Reviews
    public async Task<IActionResult> Index()
    {
        var appDbContext = context.Reviews.Include(r => r.Recipe).Include(r => r.User);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Reviews/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var review = await context.Reviews
            .Include(r => r.Recipe)
            .Include(r => r.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (review == null)
        {
            return NotFound();
        }

        return View(review);
    }

    // GET: Reviews/Create
    public IActionResult Create()
    {
        ViewData["RecipeId"] = new SelectList(context.Recipes, "Id", "Description");
        ViewData["UserId"] = new SelectList(context.Users, "Id", "Id");
        return View();
    }

    // POST: Reviews/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Edited,Rating,Comment,CreatedAt,UserId,RecipeId,Id")] Review review)
    {
        if (ModelState.IsValid)
        {
            review.Id = Guid.NewGuid();
            context.Add(review);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["RecipeId"] = new SelectList(context.Recipes, "Id", "Description", review.RecipeId);
        ViewData["UserId"] = new SelectList(context.Users, "Id", "Id", review.UserId);
        return View(review);
    }

    // GET: Reviews/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var review = await context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound();
        }
        ViewData["RecipeId"] = new SelectList(context.Recipes, "Id", "Description", review.RecipeId);
        ViewData["UserId"] = new SelectList(context.Users, "Id", "Id", review.UserId);
        return View(review);
    }

    // POST: Reviews/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Edited,Rating,Comment,CreatedAt,UserId,RecipeId,Id")] Review review)
    {
        if (id != review.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(review);
                await context.SaveChangesAsync();
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
        ViewData["RecipeId"] = new SelectList(context.Recipes, "Id", "Description", review.RecipeId);
        ViewData["UserId"] = new SelectList(context.Users, "Id", "Id", review.UserId);
        return View(review);
    }

    // GET: Reviews/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var review = await context.Reviews
            .Include(r => r.Recipe)
            .Include(r => r.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (review == null)
        {
            return NotFound();
        }

        return View(review);
    }

    // POST: Reviews/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var review = await context.Reviews.FindAsync(id);
        if (review != null)
        {
            context.Reviews.Remove(review);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ReviewExists(Guid id)
    {
        return context.Reviews.Any(e => e.Id == id);
    }
}