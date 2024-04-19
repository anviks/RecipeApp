using App.Contracts.DAL;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UnitController : Controller
{
    private readonly IAppUnitOfWork _unitOfWork;

    public UnitController(IAppUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: Unit
    public async Task<IActionResult> Index()
    {
        return View(await _unitOfWork.Units.FindAllAsync());
    }

    // GET: Unit/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Unit? unit = await _unitOfWork.Units.FindAsync(id.Value);
        if (unit == null)
        {
            return NotFound();
        }

        return View(unit);
    }

    // GET: Unit/Create
    public IActionResult Create()
    {
        ViewData["IngredientTypeId"] = new SelectList(_unitOfWork.IngredientTypes.FindAll(), "Id", "Description");
        return View();
    }

    // POST: Unit/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IngredientTypeId,Name,Abbreviation,UnitMultiplier,Id")] Unit unit)
    {
        if (ModelState.IsValid)
        {
            unit.Id = Guid.NewGuid();
            _unitOfWork.Units.Add(unit);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["IngredientTypeId"] = new SelectList(await _unitOfWork.IngredientTypes.FindAllAsync(), "Id", "Description", unit.IngredientTypeId);
        return View(unit);
    }

    // GET: Unit/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Unit? unit = await _unitOfWork.Units.FindAsync(id.Value);
        if (unit == null)
        {
            return NotFound();
        }
        ViewData["IngredientTypeId"] = new SelectList(await _unitOfWork.IngredientTypes.FindAllAsync(), "Id", "Description", unit.IngredientTypeId);
        return View(unit);
    }

    // POST: Unit/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("IngredientTypeId,Name,Abbreviation,UnitMultiplier,Id")] Unit unit)
    {
        if (id != unit.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _unitOfWork.Units.Update(unit);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.Units.ExistsAsync(unit.Id))
                {
                    return NotFound();
                }

                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["IngredientTypeId"] = new SelectList(await _unitOfWork.IngredientTypes.FindAllAsync(), "Id", "Description", unit.IngredientTypeId);
        return View(unit);
    }

    // GET: Unit/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Unit? unit = await _unitOfWork.Units.FindAsync(id.Value);
        if (unit == null)
        {
            return NotFound();
        }

        return View(unit);
    }

    // POST: Unit/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        Unit? unit = await _unitOfWork.Units.FindAsync(id);
        if (unit != null)
        {
            await _unitOfWork.Units.RemoveAsync(unit);
        }

        await _unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}