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
public class RecipesController(AppDbContext context) : Controller
{
    // GET: Recipes
    public async Task<IActionResult> Index()
    {
        var appDbContext = context.Recipes.Include(r => r.AuthorUser).Include(r => r.UpdatingUser);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Recipes/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await context.Recipes
            .Include(r => r.AuthorUser)
            .Include(r => r.UpdatingUser)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // GET: Recipes/Create
    public IActionResult Create()
    {
        ViewData["AuthorUserId"] = new SelectList(context.Users, "Id", "Id");
        ViewData["UpdatingUserId"] = new SelectList(context.Users, "Id", "Id");
        return View();
    }

    // POST: Recipes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Description,ImageFileUrl,Instructions,PreparationTime,CookingTime,Servings,IsVegetarian,IsVegan,IsGlutenFree,CreatedAt,AuthorUserId,UpdatedAt,UpdatingUserId,Id")] Recipe recipe)
    {
        if (ModelState.IsValid)
        {
            recipe.Id = Guid.NewGuid();
            context.Add(recipe);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["AuthorUserId"] = new SelectList(context.Users, "Id", "Id", recipe.AuthorUserId);
        ViewData["UpdatingUserId"] = new SelectList(context.Users, "Id", "Id", recipe.UpdatingUserId);
        return View(recipe);
    }

    // GET: Recipes/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await context.Recipes.FindAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }
        ViewData["AuthorUserId"] = new SelectList(context.Users, "Id", "Id", recipe.AuthorUserId);
        ViewData["UpdatingUserId"] = new SelectList(context.Users, "Id", "Id", recipe.UpdatingUserId);
        return View(recipe);
    }

    // POST: Recipes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Title,Description,ImageFileUrl,Instructions,PreparationTime,CookingTime,Servings,IsVegetarian,IsVegan,IsGlutenFree,CreatedAt,AuthorUserId,UpdatedAt,UpdatingUserId,Id")] Recipe recipe)
    {
        if (id != recipe.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(recipe);
                await context.SaveChangesAsync();
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
        ViewData["AuthorUserId"] = new SelectList(context.Users, "Id", "Id", recipe.AuthorUserId);
        ViewData["UpdatingUserId"] = new SelectList(context.Users, "Id", "Id", recipe.UpdatingUserId);
        return View(recipe);
    }

    // GET: Recipes/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await context.Recipes
            .Include(r => r.AuthorUser)
            .Include(r => r.UpdatingUser)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // POST: Recipes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var recipe = await context.Recipes.FindAsync(id);
        if (recipe != null)
        {
            context.Recipes.Remove(recipe);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RecipeExists(Guid id)
    {
        return context.Recipes.Any(e => e.Id == id);
    }
}