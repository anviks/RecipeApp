using System.Security.Claims;
using App.Contracts.DAL;
using App.DAL.DTO;
using App.DAL.EF;
using App.Domain.Identity;
using Base.Contracts.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Areas.Admin.ViewModels;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RecipesController(
    IAppUnitOfWork unitOfWork,
    IWebHostEnvironment environment,
    UserManager<AppUser> userManager
) : Controller
{
    // GET: Recipe
    public async Task<IActionResult> Index()
    {
        return View(await unitOfWork.Recipes.FindAllAsync());
    }

    // GET: Recipe/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Recipe? recipe = await unitOfWork.Recipes.FindAsync(id.Value);

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
                new SelectList(unitOfWork.Users.FindAll(), nameof(AppUser.Id), nameof(AppUser.UserName)),
            UpdatingUserSelectList =
                new SelectList(unitOfWork.Users.FindAll(), nameof(AppUser.Id), nameof(AppUser.UserName))
        };
        return View(viewModel);
    }

    // POST: Recipe/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RecipeCreateEditViewModel viewModel)
    {
        viewModel.Recipe.CreatedAt = DateTime.Now.ToUniversalTime();
        viewModel.Recipe.AuthorUser = await userManager.GetUserAsync(User);
        viewModel.Recipe.ImageFileName = "";

        ModelState.Clear();
        if (TryValidateModel(viewModel))
        {
            var newFileName = Guid.NewGuid() + Path.GetExtension(viewModel.RecipeImage!.FileName);
            viewModel.Recipe.ImageFileName = newFileName;
            var uploadPath = Path.Combine(environment.WebRootPath, "uploads", "recipe-images", newFileName);

            await using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                await viewModel.RecipeImage.CopyToAsync(stream);
            }

            unitOfWork.Recipes.Add(viewModel.Recipe);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.AuthorUserSelectList = new SelectList(await unitOfWork.Users.FindAllAsync(), nameof(AppUser.Id),
            nameof(AppUser.UserName), viewModel.Recipe.AuthorUserId);
        viewModel.UpdatingUserSelectList = new SelectList(await unitOfWork.Users.FindAllAsync(), nameof(AppUser.Id),
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

        Recipe? recipe = await unitOfWork.Recipes.FindAsync(id.Value);
        if (recipe == null)
        {
            return NotFound();
        }

        var viewModel = new RecipeCreateEditViewModel
        {
            Recipe = recipe,
            AuthorUserSelectList = new SelectList(await unitOfWork.Users.FindAllAsync(), nameof(AppUser.Id),
                nameof(AppUser.UserName), recipe.AuthorUserId),
            UpdatingUserSelectList = new SelectList(await unitOfWork.Users.FindAllAsync(), nameof(AppUser.Id),
                nameof(AppUser.UserName), recipe.UpdatingUserId)
        };

        return View(viewModel);
    }

    // POST: Recipe/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, RecipeCreateEditViewModel viewModel)
    {
        if (id != viewModel.Recipe.Id)
        {
            return NotFound();
        }
    
        Recipe existingRecipe = (await unitOfWork.Recipes.FindAsync(id))!;
        Recipe recipe = viewModel.Recipe;
        recipe.CreatedAt = existingRecipe.CreatedAt;
        recipe.AuthorUserId = existingRecipe.AuthorUserId;
        recipe.UpdatedAt = DateTime.Now.ToUniversalTime();
        recipe.UpdatingUser = await userManager.GetUserAsync(User);
        recipe.ImageFileName = existingRecipe.ImageFileName;
        
        await using (FileStream stream = System.IO.File.OpenRead(Path.Combine(environment.WebRootPath, "uploads", "recipe-images", recipe.ImageFileName)))
        {
            var formFile = new FormFile(stream, 0, stream.Length, null!, recipe.ImageFileName);
            viewModel.RecipeImage = formFile;
        }
        
        ModelState.Clear();
        if (TryValidateModel(viewModel))
        {
            try
            {
                unitOfWork.Recipes.Update(viewModel.Recipe);
                await unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unitOfWork.Recipes.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        viewModel.AuthorUserSelectList = new SelectList(await unitOfWork.Users.FindAllAsync(), nameof(AppUser.Id),
            nameof(AppUser.UserName), viewModel.Recipe.AuthorUserId);
        viewModel.UpdatingUserSelectList = new SelectList(await unitOfWork.Users.FindAllAsync(), nameof(AppUser.Id),
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

        Recipe? recipe = await unitOfWork.Recipes.FindAsync(id.Value);
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
        Recipe? recipe = await unitOfWork.Recipes.FindAsync(id);
        if (recipe != null)
        {
            await unitOfWork.Recipes.RemoveAsync(recipe);
        }

        await unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}