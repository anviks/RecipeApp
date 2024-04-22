using App.Contracts.DAL;
using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using Base.Contracts.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Areas.Admin.ViewModels;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RecipeController : Controller
{
    private readonly IAppUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _environment;

    public RecipeController(IAppUnitOfWork unitOfWork, IWebHostEnvironment environment)
    {
        _unitOfWork = unitOfWork;
        _environment = environment;
    }

    // GET: Recipe
    public async Task<IActionResult> Index()
    {
        return View(await _unitOfWork.Recipes.FindAllAsync());
    }

    // GET: Recipe/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Recipe? recipe = await _unitOfWork.Recipes.FindAsync(id.Value);

        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // GET: Recipe/Create
    public IActionResult Create()
    {
        var viewModel = new RecipeCreateEditViewModel
        {
            AuthorUserSelectList =
                new SelectList(_unitOfWork.Users.FindAll(), nameof(AppUser.Id), nameof(AppUser.UserName)),
            UpdatingUserSelectList =
                new SelectList(_unitOfWork.Users.FindAll(), nameof(AppUser.Id), nameof(AppUser.UserName))
        };
        return View(viewModel);
    }

    // POST: Recipe/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    // [Bind("Title,Description,Image,Instructions,PrepationTime,CookingTime,Servings,IsVegetarian,IsVegan,IsGlutenFree,CreatedAt,AuthorUserId,UpdatedAt,UpdatingUserId,Id")]
    public async Task<IActionResult> Create(
        // [Bind("Recipe.Title,Recipe.Description,Recipe.Instructions,Recipe.PrepationTime,Recipe.CookingTime,Recipe.Servings" +
        //       ",Recipe.IsVegetarian,Recipe.IsVegan,Recipe.IsGlutenFree,Recipe.CreatedAt,Recipe.AuthorUserId,Recipe.UpdatedAt,Recipe.UpdatingUserId,Recipe.Id" +
        //       ",RecipeImage")] 
        RecipeCreateEditViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var newFileName = Guid.NewGuid() + Path.GetExtension(viewModel.RecipeImage.FileName);
            var uploadPath = Path.Combine(_environment.WebRootPath, "uploads", "recipe-images", newFileName);
            
            await using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                await viewModel.RecipeImage.CopyToAsync(stream);
            }

            viewModel.Recipe.ImageFileName = newFileName;
            _unitOfWork.Recipes.Add(viewModel.Recipe);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.AuthorUserSelectList = new SelectList(await _unitOfWork.Users.FindAllAsync(), nameof(AppUser.Id),
            nameof(AppUser.UserName), viewModel.Recipe.AuthorUserId);
        viewModel.UpdatingUserSelectList = new SelectList(await _unitOfWork.Users.FindAllAsync(), nameof(AppUser.Id),
            nameof(AppUser.UserName), viewModel.Recipe.UpdatingUserId);

        return View(viewModel);
    }

    // GET: Recipe/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Recipe? recipe = await _unitOfWork.Recipes.FindAsync(id.Value);
        if (recipe == null)
        {
            return NotFound();
        }

        var viewModel = new RecipeCreateEditViewModel
        {
            AuthorUserSelectList = new SelectList(await _unitOfWork.Users.FindAllAsync(), nameof(AppUser.Id),
                nameof(AppUser.UserName), recipe.AuthorUserId),
            UpdatingUserSelectList = new SelectList(await _unitOfWork.Users.FindAllAsync(), nameof(AppUser.Id),
                nameof(AppUser.UserName), recipe.UpdatingUserId)
        };

        return View(viewModel);
    }

    // POST: Recipe/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    // [Bind("Title,Description,Image,Instructions,PrepationTime,CookingTime,Servings,IsVegetarian,IsVegan,IsGlutenFree,CreatedAt,AuthorUserId,UpdatedAt,UpdatingUserId,Id")]
    public async Task<IActionResult> Edit(Guid id, RecipeCreateEditViewModel viewModel)
    {
        if (id != viewModel.Recipe.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _unitOfWork.Recipes.Update(viewModel.Recipe);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.Recipes.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        viewModel.AuthorUserSelectList = new SelectList(await _unitOfWork.Users.FindAllAsync(), nameof(AppUser.Id),
            nameof(AppUser.UserName), viewModel.Recipe.AuthorUserId);
        viewModel.UpdatingUserSelectList = new SelectList(await _unitOfWork.Users.FindAllAsync(), nameof(AppUser.Id),
            nameof(AppUser.UserName), viewModel.Recipe.UpdatingUserId);
        
        return View(viewModel);
    }

    // GET: Recipe/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Recipe? recipe = await _unitOfWork.Recipes.FindAsync(id.Value);
        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // POST: Recipe/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        Recipe? recipe = await _unitOfWork.Recipes.FindAsync(id);
        if (recipe != null)
        {
            await _unitOfWork.Recipes.RemoveAsync(recipe);
        }

        await _unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}