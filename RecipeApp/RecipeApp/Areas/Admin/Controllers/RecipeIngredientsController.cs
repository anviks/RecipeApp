using App.Contracts.DAL;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RecipeIngredientsController(IAppUnitOfWork unitOfWork) : Controller
{
    // GET: RecipeIngredient
    public async Task<IActionResult> Index()
    {
        return View(await unitOfWork.RecipeIngredients.FindAllAsync());
    }

    // GET: RecipeIngredient/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeIngredient? recipeIngredient = await unitOfWork.RecipeIngredients.FindAsync(id.Value);
        if (recipeIngredient == null)
        {
            return NotFound();
        }

        return View(recipeIngredient);
    }

    // GET: RecipeIngredient/Create
    public IActionResult Create()
    {
        ViewData["IngredientId"] = new SelectList(unitOfWork.Ingredients.FindAll(), "Id", "Name");
        ViewData["RecipeId"] = new SelectList(unitOfWork.Recipes.FindAll(), "Id", "Description");
        ViewData["UnitId"] = new SelectList(unitOfWork.Units.FindAll(), "Id", "Name");
        return View();
    }

    // POST: RecipeIngredient/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("RecipeId,IngredientId,UnitId,CustomUnit,Quantity,IngredientModifier,Id")] RecipeIngredient recipeIngredient)
    {
        if (ModelState.IsValid)
        {
            recipeIngredient.Id = Guid.NewGuid();
            unitOfWork.RecipeIngredients.Add(recipeIngredient);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["IngredientId"] = new SelectList(await unitOfWork.Ingredients.FindAllAsync(), "Id", "Name", recipeIngredient.IngredientId);
        ViewData["RecipeId"] = new SelectList(await unitOfWork.Recipes.FindAllAsync(), "Id", "Description", recipeIngredient.RecipeId);
        ViewData["UnitId"] = new SelectList(await unitOfWork.Units.FindAllAsync(), "Id", "Name", recipeIngredient.UnitId);
        return View(recipeIngredient);
    }

    // GET: RecipeIngredient/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeIngredient? recipeIngredient = await unitOfWork.RecipeIngredients.FindAsync(id.Value);
        if (recipeIngredient == null)
        {
            return NotFound();
        }
        ViewData["IngredientId"] = new SelectList(await unitOfWork.Ingredients.FindAllAsync(), "Id", "Name", recipeIngredient.IngredientId);
        ViewData["RecipeId"] = new SelectList(await unitOfWork.Recipes.FindAllAsync(), "Id", "Description", recipeIngredient.RecipeId);
        ViewData["UnitId"] = new SelectList(await unitOfWork.Units.FindAllAsync(), "Id", "Name", recipeIngredient.UnitId);
        return View(recipeIngredient);
    }

    // POST: RecipeIngredient/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("RecipeId,IngredientId,UnitId,CustomUnit,Quantity,IngredientModifier,Id")] RecipeIngredient recipeIngredient)
    {
        if (id != recipeIngredient.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                unitOfWork.RecipeIngredients.Update(recipeIngredient);
                await unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unitOfWork.RecipeIngredients.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["IngredientId"] = new SelectList(await unitOfWork.Ingredients.FindAllAsync(), "Id", "Name", recipeIngredient.IngredientId);
        ViewData["RecipeId"] = new SelectList(await unitOfWork.Recipes.FindAllAsync(), "Id", "Description", recipeIngredient.RecipeId);
        ViewData["UnitId"] = new SelectList(await unitOfWork.Units.FindAllAsync(), "Id", "Name", recipeIngredient.UnitId);
        return View(recipeIngredient);
    }

    // GET: RecipeIngredient/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeIngredient? recipeIngredient = await unitOfWork.RecipeIngredients.FindAsync(id.Value);
        if (recipeIngredient == null)
        {
            return NotFound();
        }

        return View(recipeIngredient);
    }

    // POST: RecipeIngredient/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        RecipeIngredient? recipeIngredient = await unitOfWork.RecipeIngredients.FindAsync(id);
        if (recipeIngredient != null)
        {
            await unitOfWork.RecipeIngredients.RemoveAsync(recipeIngredient);
        }

        await unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}