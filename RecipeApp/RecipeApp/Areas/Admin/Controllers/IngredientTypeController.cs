using App.Contracts.DAL;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class IngredientTypeController : Controller
{
    private readonly IAppUnitOfWork _unitOfWork;

    public IngredientTypeController(IAppUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: IngredientType
    public async Task<IActionResult> Index()
    {
        return View(await _unitOfWork.IngredientTypes.FindAllAsync());
    }

    // GET: IngredientType/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        IngredientType? ingredientType = await _unitOfWork.IngredientTypes.FindAsync(id.Value);
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
        _unitOfWork.IngredientTypes.Add(ingredientType);
        await _unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: IngredientType/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        IngredientType? ingredientType = await _unitOfWork.IngredientTypes.FindAsync(id.Value);
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
            _unitOfWork.IngredientTypes.Update(ingredientType);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _unitOfWork.IngredientTypes.ExistsAsync(ingredientType.Id))
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

        IngredientType? ingredientType = await _unitOfWork.IngredientTypes.FindAsync(id.Value);
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
        IngredientType? ingredientType = await _unitOfWork.IngredientTypes.FindAsync(id);
        if (ingredientType != null)
        {
            await _unitOfWork.IngredientTypes.RemoveAsync(ingredientType);
        }

        await _unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}