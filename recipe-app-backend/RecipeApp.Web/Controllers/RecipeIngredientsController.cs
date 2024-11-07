using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Contracts;
using RecipeApp.Application.DTO;
using RecipeApp.Web.ViewModels;

namespace RecipeApp.Web.Controllers;

[Authorize]
public class RecipeIngredientsController(IAppBusinessLogic businessLogic) : Controller
{
    // GET: RecipeIngredient
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var recipeIngredients = await businessLogic.RecipeIngredients.GetAllAsync();
        var recipeIngredientsViewModels = new List<RecipeIngredientDetailsViewModel>();
        foreach (RecipeIngredient recipeIngredient in recipeIngredients)
        {
            Ingredient? ingredient = await businessLogic.Ingredients.GetByIdAsync(recipeIngredient.IngredientId);
            RecipeResponse? recipe = await businessLogic.Recipes.GetByIdAsync(recipeIngredient.RecipeId);
            Unit? unit = await businessLogic.Units.GetByIdAsync(recipeIngredient.UnitId ?? Guid.Empty);

            recipeIngredientsViewModels.Add(new RecipeIngredientDetailsViewModel
            {
                RecipeIngredient = recipeIngredient,
                IngredientName = ingredient!.Name,
                RecipeName = recipe!.Title,
                UnitName = unit?.Name ?? recipeIngredient.CustomUnit!
            });
        }

        return View(recipeIngredientsViewModels);
    }

    // GET: RecipeIngredient/Details/5
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeIngredient? recipeIngredient = await businessLogic.RecipeIngredients.GetByIdAsync(id.Value);
        if (recipeIngredient == null)
        {
            return NotFound();
        }

        Ingredient? ingredient = await businessLogic.Ingredients.GetByIdAsync(recipeIngredient.IngredientId);
        RecipeResponse? recipe = await businessLogic.Recipes.GetByIdAsync(recipeIngredient.RecipeId);
        Unit? unit = await businessLogic.Units.GetByIdAsync(recipeIngredient.UnitId ?? Guid.Empty);

        var recipeIngredientViewModel = new RecipeIngredientDetailsViewModel
        {
            RecipeIngredient = recipeIngredient,
            IngredientName = ingredient!.Name,
            RecipeName = recipe!.Title,
            UnitName = unit?.Name ?? recipeIngredient.CustomUnit!
        };

        return View(recipeIngredientViewModel);
    }

    // GET: RecipeIngredient/Create
    public async Task<IActionResult> Create()
    {
        var viewModel = new RecipeIngredientCreateEditViewModel
        {
            IngredientSelectList = new SelectList(await businessLogic.Ingredients.GetAllAsync(), nameof(Ingredient.Id),
                nameof(Ingredient.Name)),
            RecipeSelectList = new SelectList(await businessLogic.Recipes.GetAllAsync(), nameof(RecipeResponse.Id),
                nameof(RecipeResponse.Title)),
            UnitSelectList = new SelectList(await businessLogic.Units.GetAllAsync(), nameof(Unit.Id),
                nameof(Unit.Name))
        };
        
        return View(viewModel);
    }

    // POST: RecipeIngredient/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RecipeIngredientCreateEditViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            RecipeIngredient recipeIngredient = viewModel.RecipeIngredient;
            recipeIngredient.Id = Guid.NewGuid();
            businessLogic.RecipeIngredients.AddAsync(recipeIngredient);
            await businessLogic.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.IngredientSelectList = new SelectList(await businessLogic.Ingredients.GetAllAsync(),
            nameof(Ingredient.Id),
            nameof(Ingredient.Name));
        viewModel.RecipeSelectList = new SelectList(await businessLogic.Recipes.GetAllAsync(),
            nameof(RecipeResponse.Id),
            nameof(RecipeResponse.Title));
        viewModel.UnitSelectList = new SelectList(await businessLogic.Units.GetAllAsync(), 
            nameof(Unit.Id),
            nameof(Unit.Name));

        return View(viewModel);
    }

    // GET: RecipeIngredient/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeIngredient? recipeIngredient = await businessLogic.RecipeIngredients.GetByIdAsync(id.Value);
        if (recipeIngredient == null)
        {
            return NotFound();
        }

        var viewModel = new RecipeIngredientCreateEditViewModel
        {
            RecipeIngredient = recipeIngredient,
            IngredientSelectList = new SelectList(await businessLogic.Ingredients.GetAllAsync(), nameof(Ingredient.Id),
                nameof(Ingredient.Name), recipeIngredient.IngredientId),
            RecipeSelectList = new SelectList(await businessLogic.Recipes.GetAllAsync(), nameof(RecipeResponse.Id),
                nameof(RecipeResponse.Title), recipeIngredient.RecipeId),
            UnitSelectList = new SelectList(await businessLogic.Units.GetAllAsync(), nameof(Unit.Id),
                nameof(Unit.Name), recipeIngredient.UnitId)
        };
        return View(viewModel);
    }

    // POST: RecipeIngredient/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, RecipeIngredientCreateEditViewModel viewModel)
    {
        RecipeIngredient recipeIngredient = viewModel.RecipeIngredient;
        if (id != recipeIngredient.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                businessLogic.RecipeIngredients.UpdateAsync(recipeIngredient);
                await businessLogic.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await businessLogic.RecipeIngredients.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        viewModel.IngredientSelectList = new SelectList(await businessLogic.Ingredients.GetAllAsync(),
            nameof(Ingredient.Id),
            nameof(Ingredient.Name), recipeIngredient.IngredientId);
        viewModel.RecipeSelectList = new SelectList(await businessLogic.Recipes.GetAllAsync(),
            nameof(RecipeResponse.Id),
            nameof(RecipeResponse.Title), recipeIngredient.RecipeId);
        viewModel.UnitSelectList = new SelectList(await businessLogic.Units.GetAllAsync(), nameof(Unit.Id),
            nameof(Unit.Name), recipeIngredient.UnitId);

        return View(viewModel);
    }

    // GET: RecipeIngredient/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeIngredient? recipeIngredient = await businessLogic.RecipeIngredients.GetByIdAsync(id.Value);
        if (recipeIngredient == null)
        {
            return NotFound();
        }

        Ingredient? ingredient = await businessLogic.Ingredients.GetByIdAsync(recipeIngredient.IngredientId);
        RecipeResponse? recipe = await businessLogic.Recipes.GetByIdAsync(recipeIngredient.RecipeId);
        Unit? unit = await businessLogic.Units.GetByIdAsync(recipeIngredient.UnitId ?? Guid.Empty);

        var viewModel = new RecipeIngredientDetailsViewModel
        {
            RecipeIngredient = recipeIngredient,
            IngredientName = ingredient!.Name,
            RecipeName = recipe!.Title,
            UnitName = unit?.Name ?? recipeIngredient.CustomUnit!
        };

        return View(viewModel);
    }

    // POST: RecipeIngredient/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        RecipeIngredient? recipeIngredient = await businessLogic.RecipeIngredients.GetByIdAsync(id);
        if (recipeIngredient != null)
        {
            await businessLogic.RecipeIngredients.DeleteAsync(recipeIngredient);
        }

        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}