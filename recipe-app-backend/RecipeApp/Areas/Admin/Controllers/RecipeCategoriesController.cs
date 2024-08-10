using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RecipeCategoriesController(AppDbContext context) : Controller
{
    // GET: RecipeCategorys
    public async Task<IActionResult> Index()
    {
        var appDbContext = context.RecipeCategories.Include(r => r.Category).Include(r => r.Recipe);
        return View(await appDbContext.ToListAsync());
    }

    // GET: RecipeCategorys/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipeCategory = await context.RecipeCategories
            .Include(r => r.Category)
            .Include(r => r.Recipe)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (recipeCategory == null)
        {
            return NotFound();
        }

        return View(recipeCategory);
    }

    // GET: RecipeCategorys/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Id");
        ViewData["RecipeId"] = new SelectList(context.Recipes, "Id", "Description");
        return View();
    }

    // POST: RecipeCategorys/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CategoryId,RecipeId,Id")] RecipeCategory recipeCategory)
    {
        if (ModelState.IsValid)
        {
            recipeCategory.Id = Guid.NewGuid();
            context.Add(recipeCategory);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Id", recipeCategory.CategoryId);
        ViewData["RecipeId"] = new SelectList(context.Recipes, "Id", "Description", recipeCategory.RecipeId);
        return View(recipeCategory);
    }

    // GET: RecipeCategorys/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipeCategory = await context.RecipeCategories.FindAsync(id);
        if (recipeCategory == null)
        {
            return NotFound();
        }
        ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Id", recipeCategory.CategoryId);
        ViewData["RecipeId"] = new SelectList(context.Recipes, "Id", "Description", recipeCategory.RecipeId);
        return View(recipeCategory);
    }

    // POST: RecipeCategorys/Edit/5
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
                context.Update(recipeCategory);
                await context.SaveChangesAsync();
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
        ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Id", recipeCategory.CategoryId);
        ViewData["RecipeId"] = new SelectList(context.Recipes, "Id", "Description", recipeCategory.RecipeId);
        return View(recipeCategory);
    }

    // GET: RecipeCategorys/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipeCategory = await context.RecipeCategories
            .Include(r => r.Category)
            .Include(r => r.Recipe)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (recipeCategory == null)
        {
            return NotFound();
        }

        return View(recipeCategory);
    }

    // POST: RecipeCategorys/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var recipeCategory = await context.RecipeCategories.FindAsync(id);
        if (recipeCategory != null)
        {
            context.RecipeCategories.Remove(recipeCategory);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RecipeCategoryExists(Guid id)
    {
        return context.RecipeCategories.Any(e => e.Id == id);
    }
}