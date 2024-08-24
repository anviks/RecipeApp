using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Contracts;
using RecipeApp.Application.DTO;

namespace RecipeApp.Web.Controllers;

[Authorize(Roles = "Admin")]
public class IngredientTypesController(IAppBusinessLogic businessLogic) : Controller
{
    // GET: IngredientType
    
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var ingredientTypes = await businessLogic.IngredientTypes.FindAllAsync();
        return View(ingredientTypes);
    }

    // GET: IngredientType/Details/5
    
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        IngredientType? ingredientType = await businessLogic.IngredientTypes.FindAsync(id.Value);
        if (ingredientType == null)
        {
            return NotFound();
        }

        return View(ingredientType);
    }

    // GET: IngredientType/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: IngredientType/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,Id")] IngredientType ingredientType)
    {
        if (!ModelState.IsValid) return View(ingredientType);
        
        ingredientType.Id = Guid.NewGuid();
        businessLogic.IngredientTypes.Add(ingredientType);
        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: IngredientType/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        IngredientType? ingredientType = await businessLogic.IngredientTypes.FindAsync(id.Value);
        if (ingredientType == null)
        {
            return NotFound();
        }
        return View(ingredientType);
    }

    // POST: IngredientType/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Id")] IngredientType ingredientType)
    {
        if (id != ingredientType.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid) return View(ingredientType);
        try
        {
            businessLogic.IngredientTypes.Update(ingredientType);
            await businessLogic.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await businessLogic.IngredientTypes.ExistsAsync(ingredientType.Id))
            {
                return NotFound();
            }

            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: IngredientType/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        IngredientType? ingredientType = await businessLogic.IngredientTypes.FindAsync(id.Value);
        if (ingredientType == null)
        {
            return NotFound();
        }

        return View(ingredientType);
    }

    // POST: IngredientType/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        IngredientType? ingredientType = await businessLogic.IngredientTypes.FindAsync(id);
        if (ingredientType != null)
        {
            await businessLogic.IngredientTypes.RemoveAsync(ingredientType);
        }

        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}