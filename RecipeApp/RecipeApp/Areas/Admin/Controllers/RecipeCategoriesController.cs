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
public class RecipeCategoriesController(IAppBusinessLogic businessLogic) : Controller
{
    // GET: RecipeCategory
    public async Task<IActionResult> Index()
    {
        var recipeCategories = await businessLogic.RecipeCategories.FindAllAsync();
        var viewModels = new List<RecipeCategoryDetailsViewModel>();

        foreach (RecipeCategory recipeCategory in recipeCategories)
        {
            Category? category = await businessLogic.Categories.FindAsync(recipeCategory.CategoryId);
            RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(recipeCategory.RecipeId);
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
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeCategory? recipeCategory = await businessLogic.RecipeCategories.FindAsync(id.Value);
        if (recipeCategory == null)
        {
            return NotFound();
        }

        Category? category = await businessLogic.Categories.FindAsync(recipeCategory.CategoryId);
        RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(recipeCategory.RecipeId);
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
            CategorySelectList = new SelectList(await businessLogic.Categories.FindAllAsync(), nameof(Category.Id),
                nameof(Category.Name)),
            RecipeSelectList = new SelectList(await businessLogic.Recipes.FindAllAsync(), nameof(RecipeResponse.Id),
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
            businessLogic.RecipeCategories.Add(recipeCategory);
            await businessLogic.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.CategorySelectList = new SelectList(await businessLogic.Categories.FindAllAsync(),
            nameof(Category.Id), nameof(Category.Name));
        viewModel.RecipeSelectList = new SelectList(await businessLogic.Recipes.FindAllAsync(),
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

        RecipeCategory? recipeCategory = await businessLogic.RecipeCategories.FindAsync(id.Value);
        if (recipeCategory == null)
        {
            return NotFound();
        }

        var viewModel = new RecipeCategoryCreateEditViewModel
        {
            RecipeCategory = recipeCategory,
            CategorySelectList = new SelectList(await businessLogic.Categories.FindAllAsync(), nameof(Category.Id),
                nameof(Category.Name), recipeCategory.CategoryId),
            RecipeSelectList = new SelectList(await businessLogic.Recipes.FindAllAsync(), nameof(RecipeResponse.Id),
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
                businessLogic.RecipeCategories.Update(recipeCategory);
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

        viewModel.CategorySelectList = new SelectList(await businessLogic.Categories.FindAllAsync(), nameof(Category.Id),
            nameof(Category.Name), recipeCategory.CategoryId);
        viewModel.RecipeSelectList = new SelectList(await businessLogic.Recipes.FindAllAsync(), nameof(RecipeResponse.Id),
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

        RecipeCategory? recipeCategory = await businessLogic.RecipeCategories.FindAsync(id.Value);
        if (recipeCategory == null)
        {
            return NotFound();
        }
        
        Category? category = await businessLogic.Categories.FindAsync(recipeCategory.CategoryId);
        RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(recipeCategory.RecipeId);
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
        RecipeCategory? recipeCategory = await businessLogic.RecipeCategories.FindAsync(id);
        if (recipeCategory != null)
        {
            await businessLogic.RecipeCategories.RemoveAsync(recipeCategory);
        }

        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}