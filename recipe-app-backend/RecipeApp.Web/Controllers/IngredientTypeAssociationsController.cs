using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Contracts;
using RecipeApp.Application.DTO;
using RecipeApp.Web.ViewModels;

namespace RecipeApp.Web.Controllers;

[Authorize(Roles = "Admin")]
public class IngredientTypeAssociationsController(IAppBusinessLogic businessLogic) : Controller
{
    // GET: IngredientTypeAssociation
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var associations = await businessLogic.IngredientTypeAssociations.GetAllAsync();
        var associationsViewModels = new List<IngredientTypeAssociationDetailsViewModel>();
        foreach (IngredientTypeAssociation typeAssociation in associations)
        {
            Ingredient? ingredient = await businessLogic.Ingredients.GetByIdAsync(typeAssociation.IngredientId);
            IngredientType? ingredientType = await businessLogic.IngredientTypes.GetByIdAsync(typeAssociation.IngredientTypeId);
            associationsViewModels.Add(new IngredientTypeAssociationDetailsViewModel
            {
                IngredientTypeAssociation = typeAssociation,
                IngredientName = ingredient!.Name,
                IngredientTypeName = ingredientType!.Name
            });
        }
        return View(associationsViewModels);
    }

    // GET: IngredientTypeAssociation/Details/5
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        IngredientTypeAssociation? ingredientTypeAssociation = await businessLogic.IngredientTypeAssociations.GetByIdAsync(id.Value);
        if (ingredientTypeAssociation == null)
        {
            return NotFound();
        }
        
        Ingredient? ingredient = await businessLogic.Ingredients.GetByIdAsync(ingredientTypeAssociation.IngredientId);
        IngredientType? ingredientType = await businessLogic.IngredientTypes.GetByIdAsync(ingredientTypeAssociation.IngredientTypeId);
        var associationViewModel = new IngredientTypeAssociationDetailsViewModel
        {
            IngredientTypeAssociation = ingredientTypeAssociation,
            IngredientName = ingredient!.Name,
            IngredientTypeName = ingredientType!.Name
        };

        return View(associationViewModel);
    }

    // GET: IngredientTypeAssociation/Create
    public async Task<IActionResult> Create()
    {
        var viewModel = new IngredientTypeAssociationCreateEditViewModel
        {
            IngredientSelectList = new SelectList(await businessLogic.Ingredients.GetAllAsync(), nameof(Ingredient.Id),
                nameof(Ingredient.Name)),
            IngredientTypeSelectList = new SelectList(await businessLogic.IngredientTypes.GetAllAsync(), nameof(IngredientType.Id),
                nameof(IngredientType.Name))
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
            IngredientTypeAssociation association = viewModel.IngredientTypeAssociation;
            association.Id = Guid.NewGuid();
            businessLogic.IngredientTypeAssociations.AddAsync(association);
            await businessLogic.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.IngredientSelectList = new SelectList(await businessLogic.Ingredients.GetAllAsync(),
            nameof(Ingredient.Id), nameof(Ingredient.Name), viewModel.IngredientTypeAssociation.IngredientId);
        viewModel.IngredientTypeSelectList = new SelectList(await businessLogic.IngredientTypes.GetAllAsync(),
            nameof(IngredientType.Id), nameof(IngredientType.Name),
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
            await businessLogic.IngredientTypeAssociations.GetByIdAsync(id.Value);
        if (ingredientTypeAssociation == null)
        {
            return NotFound();
        }

        var viewModel = new IngredientTypeAssociationCreateEditViewModel
        {
            IngredientTypeAssociation = ingredientTypeAssociation,
            IngredientSelectList = new SelectList(await businessLogic.Ingredients.GetAllAsync(), nameof(Ingredient.Id),
                nameof(Ingredient.Name), ingredientTypeAssociation.IngredientId),
            IngredientTypeSelectList = new SelectList(await businessLogic.IngredientTypes.GetAllAsync(),
                nameof(IngredientType.Id), nameof(IngredientType.Name),
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
                businessLogic.IngredientTypeAssociations.UpdateAsync(viewModel.IngredientTypeAssociation);
                await businessLogic.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await businessLogic.IngredientTypeAssociations.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        viewModel.IngredientSelectList = new SelectList(await businessLogic.Ingredients.GetAllAsync(),
            nameof(Ingredient.Id),
            nameof(Ingredient.Name),
            viewModel.IngredientTypeAssociation.IngredientId);
        viewModel.IngredientTypeSelectList = new SelectList(await businessLogic.IngredientTypes.GetAllAsync(),
            nameof(IngredientType.Id),
            nameof(IngredientType.Name),
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
            await businessLogic.IngredientTypeAssociations.GetByIdAsync(id.Value);
        if (ingredientTypeAssociation == null)
        {
            return NotFound();
        }
        
        Ingredient? ingredient = await businessLogic.Ingredients.GetByIdAsync(ingredientTypeAssociation.IngredientId);
        IngredientType? ingredientType = await businessLogic.IngredientTypes.GetByIdAsync(ingredientTypeAssociation.IngredientTypeId);
        var associationViewModel = new IngredientTypeAssociationDetailsViewModel
        {
            IngredientTypeAssociation = ingredientTypeAssociation,
            IngredientName = ingredient!.Name,
            IngredientTypeName = ingredientType!.Name
        };

        return View(associationViewModel);
    }

    // POST: IngredientTypeAssociation/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        IngredientTypeAssociation? ingredientTypeAssociation =
            await businessLogic.IngredientTypeAssociations.GetByIdAsync(id);
        if (ingredientTypeAssociation != null)
        {
            await businessLogic.IngredientTypeAssociations.DeleteAsync(ingredientTypeAssociation);
        }

        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}