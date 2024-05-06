using System.Security.Claims;
using App.Contracts.BLL;
using App.Contracts.DAL;
using App.DAL.EF;
using App.Domain.Identity;
using Base.Contracts.DAL;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Areas.Admin.ViewModels;
using DAL_DTO = App.DAL.DTO;
using BLL_DTO = App.BLL.DTO;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RecipesController(
    IAppBusinessLogic businessLogic,
    EntityMapper<BLL_DTO.RecipeRequest, DAL_DTO.Recipe> requestDalMapper,
    EntityMapper<BLL_DTO.RecipeRequest, BLL_DTO.RecipeResponse> requestResponseMapper,
    IWebHostEnvironment environment,
    UserManager<AppUser> userManager
) : Controller
{
    // GET: Recipe
    public async Task<IActionResult> Index()
    {
        var allRecipes = await businessLogic.Recipes.FindAllAsync();
        return View(allRecipes);
    }

    // GET: Recipe/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        BLL_DTO.RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(id.Value);

        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // GET: Recipe/Create
    public IActionResult Create()
    {
        return View(new RecipeCreateEditViewModel());
    }

    // POST: Recipe/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RecipeCreateEditViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);
        
        businessLogic.Recipes.Add(viewModel.RecipeRequest, Guid.Parse(userManager.GetUserId(User)!));
        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Recipe/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        BLL_DTO.RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(id.Value);
        if (recipe == null)
        {
            return NotFound();
        }

        var viewModel = new RecipeCreateEditViewModel
        {
            RecipeRequest = requestResponseMapper.Map(recipe)!
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
        if (id != viewModel.RecipeRequest.Id)
        {
            return NotFound();
        }
    
        BLL_DTO.RecipeResponse existingRecipe = (await businessLogic.Recipes.FindAsync(id))!;
        DAL_DTO.Recipe newRecipe = requestDalMapper.Map(viewModel.RecipeRequest)!;
        newRecipe.CreatedAt = existingRecipe.CreatedAt;
        newRecipe.AuthorUserId = (await userManager.FindByNameAsync(existingRecipe.AuthorUser))!.Id;
        newRecipe.UpdatedAt = DateTime.Now.ToUniversalTime();
        newRecipe.UpdatingUserId = (await userManager.GetUserAsync(User))!.Id;
        newRecipe.ImageFileName = existingRecipe.ImageFileName;
        
        ModelState.Clear();
        if (TryValidateModel(viewModel))
        {
            try
            {
                businessLogic.Recipes.Update(requestResponseMapper.Map(requestDalMapper.Map(newRecipe))!);
                await businessLogic.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await businessLogic.Recipes.ExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(viewModel);
    }

    // GET: Recipe/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        BLL_DTO.RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(id.Value);
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
        BLL_DTO.RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(id);
        if (recipe != null)
        {
            await businessLogic.Recipes.RemoveAsync(recipe);
        }

        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}