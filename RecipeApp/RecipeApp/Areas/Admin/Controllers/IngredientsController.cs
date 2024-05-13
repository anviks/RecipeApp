using App.BLL.DTO;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class IngredientsController(IAppBusinessLogic businessLogic) : Controller
{
    // GET: Ingredient
    public async Task<IActionResult> Index()
    {
        var ingredients = await businessLogic.Ingredients.FindAllAsync();
        return View(ingredients);
    }

    // GET: Ingredient/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Ingredient? ingredient = await businessLogic.Ingredients.FindAsync(id.Value);
        if (ingredient == null)
        {
            return NotFound();
        }

        return View(ingredient);
    }

    // GET: Ingredient/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Ingredient/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Id")] Ingredient ingredient)
    {
        if (!ModelState.IsValid) return View(ingredient);
        businessLogic.Ingredients.Add(ingredient);
        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Ingredient/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Ingredient? ingredient = await businessLogic.Ingredients.FindAsync(id.Value);
        if (ingredient == null)
        {
            return NotFound();
        }
        return View(ingredient);
    }

    // POST: Ingredient/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] Ingredient ingredient)
    {
        if (id != ingredient.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid) return View(ingredient);
        try
        {
            businessLogic.Ingredients.Update(ingredient);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.Ingredients.ExistsAsync(id))
            {
                return NotFound();
            }

            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Ingredient/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Ingredient? ingredient = await businessLogic.Ingredients.FindAsync(id.Value);
        if (ingredient == null)
        {
            return NotFound();
        }

        return View(ingredient);
    }

    // POST: Ingredient/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        Ingredient? ingredient = await businessLogic.Ingredients.FindAsync(id);
        if (ingredient != null)
        {
            await businessLogic.Ingredients.RemoveAsync(ingredient);
        }

        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}