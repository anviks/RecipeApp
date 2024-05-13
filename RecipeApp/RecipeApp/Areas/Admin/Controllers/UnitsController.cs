using App.BLL.DTO;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Areas.Admin.ViewModels;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UnitsController(IAppBusinessLogic businessLogic) : Controller
{
    // GET: Unit
    public async Task<IActionResult> Index()
    {
        var units = await businessLogic.Units.FindAllAsync();
        var viewModels = new List<UnitDetailsViewModel>();
        foreach (Unit unit in units)
        {
            IngredientType? ingredientType = await businessLogic.IngredientTypes.FindAsync(unit.IngredientTypeId);
            viewModels.Add(new UnitDetailsViewModel
            {
                Unit = unit,
                IngredientTypeName = ingredientType!.Name
            });
        }

        return View(viewModels);
    }

    // GET: Unit/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Unit? unit = await businessLogic.Units.FindAsync(id.Value);
        if (unit == null)
        {
            return NotFound();
        }

        IngredientType? ingredientType = await businessLogic.IngredientTypes.FindAsync(unit.IngredientTypeId);
        var viewModel = new UnitDetailsViewModel
        {
            Unit = unit,
            IngredientTypeName = ingredientType!.Description
        };

        return View(viewModel);
    }

    // GET: Unit/Create
    public async Task<IActionResult> Create()
    {
        var viewModel = new UnitCreateEditViewModel
        {
            IngredientTypeSelectList = new SelectList(await businessLogic.IngredientTypes.FindAllAsync(),
                nameof(IngredientType.Id), nameof(IngredientType.Name))
        };
        return View(viewModel);
    }

    // POST: Unit/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UnitCreateEditViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            Unit unit = viewModel.Unit;
            unit.Id = Guid.NewGuid();
            businessLogic.Units.Add(unit);
            await businessLogic.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.IngredientTypeSelectList = new SelectList(await businessLogic.IngredientTypes.FindAllAsync(),
            nameof(IngredientType.Id), nameof(IngredientType.Name));
        return View(viewModel);
    }

    // GET: Unit/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Unit? unit = await businessLogic.Units.FindAsync(id.Value);
        if (unit == null)
        {
            return NotFound();
        }

        var viewModel = new UnitCreateEditViewModel
        {
            Unit = unit,
            IngredientTypeSelectList = new SelectList(await businessLogic.IngredientTypes.FindAllAsync(),
                nameof(IngredientType.Id), nameof(IngredientType.Name))
        };
        
        return View(viewModel);
    }

    // POST: Unit/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UnitCreateEditViewModel viewModel)
    {
        Unit unit = viewModel.Unit;
        if (id != unit.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                businessLogic.Units.Update(unit);
                await businessLogic.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await businessLogic.Units.ExistsAsync(unit.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        viewModel.IngredientTypeSelectList = new SelectList(await businessLogic.IngredientTypes.FindAllAsync(),
            nameof(IngredientType.Id), nameof(IngredientType.Name));
        return View(viewModel);
    }

    // GET: Unit/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Unit? unit = await businessLogic.Units.FindAsync(id.Value);
        if (unit == null)
        {
            return NotFound();
        }
        
        var viewModel = new UnitDetailsViewModel
        {
            Unit = unit,
            IngredientTypeName = (await businessLogic.IngredientTypes.FindAsync(unit.IngredientTypeId))!.Name
        };

        return View(viewModel);
    }

    // POST: Unit/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        Unit? unit = await businessLogic.Units.FindAsync(id);
        if (unit != null)
        {
            await businessLogic.Units.RemoveAsync(unit);
        }

        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}