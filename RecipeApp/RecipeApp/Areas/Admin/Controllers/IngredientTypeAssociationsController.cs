using App.Contracts.DAL;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Areas.Admin.ViewModels;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class IngredientTypeAssociationsController(IAppUnitOfWork unitOfWork) : Controller
{
    // GET: IngredientTypeAssociation
    public async Task<IActionResult> Index()
    {
        return View(await unitOfWork.IngredientTypeAssociations.FindAllAsync());
    }

    // GET: IngredientTypeAssociation/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        IngredientTypeAssociation? ingredientTypeAssociation =
            await unitOfWork.IngredientTypeAssociations.FindAsync(id.Value);
        if (ingredientTypeAssociation == null)
        {
            return NotFound();
        }

        return View(ingredientTypeAssociation);
    }

    // GET: IngredientTypeAssociation/Create
    public IActionResult Create()
    {
        var viewModel = new IngredientTypeAssociationCreateEditViewModel
        {
            IngredientSelectList = new SelectList(unitOfWork.Ingredients.FindAll(), nameof(Ingredient.Id),
                nameof(Ingredient.Name)),
            IngredientTypeSelectList = new SelectList(unitOfWork.IngredientTypes.FindAll(), nameof(IngredientType.Id),
                nameof(IngredientType.Description))
        };
        return View(viewModel);
    }

    // POST: IngredientTypeAssociation/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(IngredientTypeAssociationCreateEditViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            unitOfWork.IngredientTypeAssociations.Add(viewModel.IngredientTypeAssociation);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.IngredientSelectList = new SelectList(await unitOfWork.Ingredients.FindAllAsync(),
            nameof(Ingredient.Id), nameof(Ingredient.Name), viewModel.IngredientTypeAssociation.IngredientId);
        viewModel.IngredientTypeSelectList = new SelectList(await unitOfWork.IngredientTypes.FindAllAsync(),
            nameof(IngredientType.Id), nameof(IngredientType.Description),
            viewModel.IngredientTypeAssociation.IngredientTypeId);
        return View(viewModel);
    }

    // GET: IngredientTypeAssociation/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        IngredientTypeAssociation? ingredientTypeAssociation =
            await unitOfWork.IngredientTypeAssociations.FindAsync(id.Value);
        if (ingredientTypeAssociation == null)
        {
            return NotFound();
        }

        var viewModel = new IngredientTypeAssociationCreateEditViewModel
        {
            IngredientTypeAssociation = ingredientTypeAssociation,
            IngredientSelectList = new SelectList(await unitOfWork.Ingredients.FindAllAsync(), nameof(Ingredient.Id),
                nameof(Ingredient.Name), ingredientTypeAssociation.IngredientId),
            IngredientTypeSelectList = new SelectList(await unitOfWork.IngredientTypes.FindAllAsync(),
                nameof(IngredientType.Id), nameof(IngredientType.Description),
                ingredientTypeAssociation.IngredientTypeId)
        };

        return View(viewModel);
    }

    // POST: IngredientTypeAssociation/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, IngredientTypeAssociationCreateEditViewModel viewModel)
    {
        if (id != viewModel.IngredientTypeAssociation.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                unitOfWork.IngredientTypeAssociations.Update(viewModel.IngredientTypeAssociation);
                await unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unitOfWork.IngredientTypeAssociations.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        viewModel.IngredientSelectList = new SelectList(await unitOfWork.Ingredients.FindAllAsync(), 
            nameof(Ingredient.Id), 
            nameof(Ingredient.Name), 
            viewModel.IngredientTypeAssociation.IngredientId);
        viewModel.IngredientTypeSelectList = new SelectList(await unitOfWork.IngredientTypes.FindAllAsync(), 
            nameof(IngredientType.Id), 
            nameof(IngredientType.Description), 
            viewModel.IngredientTypeAssociation.IngredientTypeId);
        
        return View(viewModel);
    }

    // GET: IngredientTypeAssociation/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        IngredientTypeAssociation? ingredientTypeAssociation =
            await unitOfWork.IngredientTypeAssociations.FindAsync(id.Value);
        if (ingredientTypeAssociation == null)
        {
            return NotFound();
        }

        return View(ingredientTypeAssociation);
    }

    // POST: IngredientTypeAssociation/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        IngredientTypeAssociation? ingredientTypeAssociation =
            await unitOfWork.IngredientTypeAssociations.FindAsync(id);
        if (ingredientTypeAssociation != null)
        {
            await unitOfWork.IngredientTypeAssociations.RemoveAsync(ingredientTypeAssociation);
        }

        await unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}