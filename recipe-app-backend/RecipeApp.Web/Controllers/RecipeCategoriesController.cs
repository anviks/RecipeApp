using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Contracts;
using RecipeApp.Application.DTO;
using RecipeApp.Web.ViewModels;

namespace RecipeApp.Web.Controllers;

[Authorize]
public class RecipeCategoriesController(IAppBusinessLogic businessLogic) : Controller
{
    // GET: RecipeCategory
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var recipeCategories = await businessLogic.RecipeCategories.GetAllAsync();
        var viewModels = new List<RecipeCategoryDetailsViewModel>();

        foreach (RecipeCategory recipeCategory in recipeCategories)
        {
            Category? category = await businessLogic.Categories.GetByIdAsync(recipeCategory.CategoryId);
            RecipeResponse? recipe = await businessLogic.Recipes.GetByIdAsync(recipeCategory.RecipeId);
            viewModels.Add(new RecipeCategoryDetailsViewModel
            {
                RecipeCategory = recipeCategory,
                CategoryName = category!.Name,
                RecipeName = recipe!.Title
            });
        }

        return View(viewModels);
    }

    // GET: RecipeCategory/Details/5
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeCategory? recipeCategory = await businessLogic.RecipeCategories.GetByIdAsync(id.Value);
        if (recipeCategory == null)
        {
            return NotFound();
        }

        Category? category = await businessLogic.Categories.GetByIdAsync(recipeCategory.CategoryId);
        RecipeResponse? recipe = await businessLogic.Recipes.GetByIdAsync(recipeCategory.RecipeId);
        var viewModel = new RecipeCategoryDetailsViewModel
        {
            RecipeCategory = recipeCategory,
            CategoryName = category!.Name,
            RecipeName = recipe!.Title
        };

        return View(viewModel);
    }

    // GET: RecipeCategory/Create
    public async Task<IActionResult> Create()
    {
        var viewModel = new RecipeCategoryCreateEditViewModel
        {
            CategorySelectList = new SelectList(await businessLogic.Categories.GetAllAsync(), nameof(Category.Id),
                nameof(Category.Name)),
            RecipeSelectList = new SelectList(await businessLogic.Recipes.GetAllAsync(), nameof(RecipeResponse.Id),
                nameof(RecipeResponse.Title))
        };

        return View(viewModel);
    }

    // POST: RecipeCategory/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RecipeCategoryCreateEditViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            RecipeCategory recipeCategory = viewModel.RecipeCategory;
            recipeCategory.Id = Guid.NewGuid();
            businessLogic.RecipeCategories.AddAsync(recipeCategory);
            await businessLogic.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.CategorySelectList = new SelectList(await businessLogic.Categories.GetAllAsync(),
            nameof(Category.Id), nameof(Category.Name));
        viewModel.RecipeSelectList = new SelectList(await businessLogic.Recipes.GetAllAsync(),
            nameof(RecipeResponse.Id), nameof(RecipeResponse.Title));
        return View(viewModel);
    }

    // GET: RecipeCategory/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeCategory? recipeCategory = await businessLogic.RecipeCategories.GetByIdAsync(id.Value);
        if (recipeCategory == null)
        {
            return NotFound();
        }

        var viewModel = new RecipeCategoryCreateEditViewModel
        {
            RecipeCategory = recipeCategory,
            CategorySelectList = new SelectList(await businessLogic.Categories.GetAllAsync(), nameof(Category.Id),
                nameof(Category.Name), recipeCategory.CategoryId),
            RecipeSelectList = new SelectList(await businessLogic.Recipes.GetAllAsync(), nameof(RecipeResponse.Id),
                nameof(RecipeResponse.Title), recipeCategory.RecipeId)
        };
        
        return View(viewModel);
    }

    // POST: RecipeCategory/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, RecipeCategoryCreateEditViewModel viewModel)
    {
        RecipeCategory recipeCategory = viewModel.RecipeCategory;
        if (id != recipeCategory.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                businessLogic.RecipeCategories.UpdateAsync(recipeCategory);
                await businessLogic.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await businessLogic.RecipeCategories.ExistsAsync(recipeCategory.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        viewModel.CategorySelectList = new SelectList(await businessLogic.Categories.GetAllAsync(), nameof(Category.Id),
            nameof(Category.Name), recipeCategory.CategoryId);
        viewModel.RecipeSelectList = new SelectList(await businessLogic.Recipes.GetAllAsync(), nameof(RecipeResponse.Id),
            nameof(RecipeResponse.Title), recipeCategory.RecipeId);
        return View(viewModel);
    }

    // GET: RecipeCategory/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeCategory? recipeCategory = await businessLogic.RecipeCategories.GetByIdAsync(id.Value);
        if (recipeCategory == null)
        {
            return NotFound();
        }
        
        Category? category = await businessLogic.Categories.GetByIdAsync(recipeCategory.CategoryId);
        RecipeResponse? recipe = await businessLogic.Recipes.GetByIdAsync(recipeCategory.RecipeId);
        var viewModel = new RecipeCategoryDetailsViewModel
        {
            RecipeCategory = recipeCategory,
            CategoryName = category!.Name,
            RecipeName = recipe!.Title
        };

        return View(viewModel);
    }

    // POST: RecipeCategory/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        RecipeCategory? recipeCategory = await businessLogic.RecipeCategories.GetByIdAsync(id);
        if (recipeCategory != null)
        {
            await businessLogic.RecipeCategories.DeleteAsync(recipeCategory);
        }

        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}