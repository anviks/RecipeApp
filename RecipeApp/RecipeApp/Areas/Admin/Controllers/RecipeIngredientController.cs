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
public class RecipeIngredientController : Controller
{
    private readonly IAppUnitOfWork _unitOfWork;

    public RecipeIngredientController(IAppUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: RecipeIngredient
    public async Task<IActionResult> Index()
    {
        return View(await _unitOfWork.RecipeIngredients.FindAllAsync());
    }

    // GET: RecipeIngredient/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeIngredient? recipeIngredient = await _unitOfWork.RecipeIngredients.FindAsync(id.Value);
        if (recipeIngredient == null)
        {
            return NotFound();
        }

        return View(recipeIngredient);
    }

    // GET: RecipeIngredient/Create
    public IActionResult Create()
    {
        ViewData["IngredientId"] = new SelectList(_unitOfWork.Ingredients.FindAll(), "Id", "Name");
        ViewData["RecipeId"] = new SelectList(_unitOfWork.Recipes.FindAll(), "Id", "Description");
        ViewData["UnitId"] = new SelectList(_unitOfWork.Units.FindAll(), "Id", "Name");
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
            _unitOfWork.RecipeIngredients.Add(recipeIngredient);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["IngredientId"] = new SelectList(await _unitOfWork.Ingredients.FindAllAsync(), "Id", "Name", recipeIngredient.IngredientId);
        ViewData["RecipeId"] = new SelectList(await _unitOfWork.Recipes.FindAllAsync(), "Id", "Description", recipeIngredient.RecipeId);
        ViewData["UnitId"] = new SelectList(await _unitOfWork.Units.FindAllAsync(), "Id", "Name", recipeIngredient.UnitId);
        return View(recipeIngredient);
    }

    // GET: RecipeIngredient/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeIngredient? recipeIngredient = await _unitOfWork.RecipeIngredients.FindAsync(id.Value);
        if (recipeIngredient == null)
        {
            return NotFound();
        }
        ViewData["IngredientId"] = new SelectList(await _unitOfWork.Ingredients.FindAllAsync(), "Id", "Name", recipeIngredient.IngredientId);
        ViewData["RecipeId"] = new SelectList(await _unitOfWork.Recipes.FindAllAsync(), "Id", "Description", recipeIngredient.RecipeId);
        ViewData["UnitId"] = new SelectList(await _unitOfWork.Units.FindAllAsync(), "Id", "Name", recipeIngredient.UnitId);
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
                _unitOfWork.RecipeIngredients.Update(recipeIngredient);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.RecipeIngredients.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["IngredientId"] = new SelectList(await _unitOfWork.Ingredients.FindAllAsync(), "Id", "Name", recipeIngredient.IngredientId);
        ViewData["RecipeId"] = new SelectList(await _unitOfWork.Recipes.FindAllAsync(), "Id", "Description", recipeIngredient.RecipeId);
        ViewData["UnitId"] = new SelectList(await _unitOfWork.Units.FindAllAsync(), "Id", "Name", recipeIngredient.UnitId);
        return View(recipeIngredient);
    }

    // GET: RecipeIngredient/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeIngredient? recipeIngredient = await _unitOfWork.RecipeIngredients.FindAsync(id.Value);
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
        RecipeIngredient? recipeIngredient = await _unitOfWork.RecipeIngredients.FindAsync(id);
        if (recipeIngredient != null)
        {
            await _unitOfWork.RecipeIngredients.RemoveAsync(recipeIngredient);
        }

        await _unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}