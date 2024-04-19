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
public class RecipeCategoryController : Controller
{
    private readonly IAppUnitOfWork _unitOfWork;

    public RecipeCategoryController(IAppUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: RecipeCategory
    public async Task<IActionResult> Index()
    {
        return View(await _unitOfWork.RecipeCategories.FindAllAsync());
    }

    // GET: RecipeCategory/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeCategory? recipeCategory = await _unitOfWork.RecipeCategories.FindAsync(id.Value);
        if (recipeCategory == null)
        {
            return NotFound();
        }

        return View(recipeCategory);
    }

    // GET: RecipeCategory/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_unitOfWork.Categories.FindAll(), "Id", "Name");
        ViewData["RecipeId"] = new SelectList(_unitOfWork.Recipes.FindAll(), "Id", "Description");
        return View();
    }

    // POST: RecipeCategory/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CategoryId,RecipeId,Id")] RecipeCategory recipeCategory)
    {
        if (ModelState.IsValid)
        {
            recipeCategory.Id = Guid.NewGuid();
            _unitOfWork.RecipeCategories.Add(recipeCategory);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(await _unitOfWork.Categories.FindAllAsync(), "Id", "Name", recipeCategory.CategoryId);
        ViewData["RecipeId"] = new SelectList(await _unitOfWork.Recipes.FindAllAsync(), "Id", "Description", recipeCategory.RecipeId);
        return View(recipeCategory);
    }

    // GET: RecipeCategory/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeCategory? recipeCategory = await _unitOfWork.RecipeCategories.FindAsync(id.Value);
        if (recipeCategory == null)
        {
            return NotFound();
        }
        ViewData["CategoryId"] = new SelectList(await _unitOfWork.Categories.FindAllAsync(), "Id", "Name", recipeCategory.CategoryId);
        ViewData["RecipeId"] = new SelectList(await _unitOfWork.Recipes.FindAllAsync(), "Id", "Description", recipeCategory.RecipeId);
        return View(recipeCategory);
    }

    // POST: RecipeCategory/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,RecipeId,Id")] RecipeCategory recipeCategory)
    {
        if (id != recipeCategory.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _unitOfWork.RecipeCategories.Update(recipeCategory);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.RecipeCategories.ExistsAsync(recipeCategory.Id))
                {
                    return NotFound();
                }

                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(await _unitOfWork.Categories.FindAllAsync(), "Id", "Name", recipeCategory.CategoryId);
        ViewData["RecipeId"] = new SelectList(await _unitOfWork.Recipes.FindAllAsync(), "Id", "Description", recipeCategory.RecipeId);
        return View(recipeCategory);
    }

    // GET: RecipeCategory/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeCategory? recipeCategory = await _unitOfWork.RecipeCategories.FindAsync(id.Value);
        if (recipeCategory == null)
        {
            return NotFound();
        }

        return View(recipeCategory);
    }

    // POST: RecipeCategory/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        RecipeCategory? recipeCategory = await _unitOfWork.RecipeCategories.FindAsync(id);
        if (recipeCategory != null)
        {
            await _unitOfWork.RecipeCategories.RemoveAsync(recipeCategory);
        }

        await _unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}