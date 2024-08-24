using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Contracts;
using RecipeApp.Application.DTO;

namespace RecipeApp.Web.Controllers;

[Authorize(Roles = "Admin")]
public class CategoriesController(IAppBusinessLogic businessLogic) : Controller
{
    // GET: Categories
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        return View(await businessLogic.Categories.FindAllAsync());
    }

    // GET: Categories/Details/5
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Category? category = await businessLogic.Categories.FindAsync(id.Value);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // GET: Categories/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,BroadnessIndex,Id")] Category category)
    {
        if (!ModelState.IsValid) return View(category);
        
        category.Id = Guid.NewGuid();
        businessLogic.Categories.Add(category);
        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Categories/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Category? category = await businessLogic.Categories.FindAsync(id.Value);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    // POST: Categories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,BroadnessIndex,Id")] Category category)
    {
        if (id != category.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid) return View(category);
        
        try
        {
            businessLogic.Categories.Update(category);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.Categories.ExistsAsync(category.Id))
            {
                return NotFound();
            }

            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Categories/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Category? category = await businessLogic.Categories.FindAsync(id.Value);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        Category? category = await businessLogic.Categories.FindAsync(id);
        if (category != null)
        {
            await businessLogic.Categories.RemoveAsync(category);
        }

        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}