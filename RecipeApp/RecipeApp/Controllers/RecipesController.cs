using System.Security.Claims;
using App.BLL.Exceptions;
using App.Contracts.BLL;
using App.Contracts.DAL;
using App.DAL.EF;
using App.Domain.Identity;
using AutoMapper;
using Base.Contracts.DAL;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL_DTO = App.DAL.DTO;
using BLL_DTO = App.BLL.DTO;

namespace RecipeApp.Controllers;

// [Area("Admin")]
[Authorize]
public class RecipesController(
    IAppBusinessLogic businessLogic,
    IMapper mapper,
    UserManager<AppUser> userManager,
    IWebHostEnvironment environment
) : Controller
{
    private readonly EntityMapper<BLL_DTO.RecipeRequest, DAL_DTO.Recipe> _requestDalMapper = new(mapper);
    private readonly EntityMapper<BLL_DTO.RecipeRequest, BLL_DTO.RecipeResponse> _requestResponseMapper = new(mapper);

    // GET: Recipe
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var allRecipes = await businessLogic.Recipes.FindAllAsync();
        return View(allRecipes);
    }

    // GET: Recipe/Details/5
    [AllowAnonymous]
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
        return View();
    }

    // POST: Recipe/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BLL_DTO.RecipeRequest request)
    {
        if (!ModelState.IsValid) return View(request);

        try
        {
            await businessLogic.Recipes.AddAsync(
                request,
                Guid.Parse(userManager.GetUserId(User)!),
                environment.WebRootPath);
        }
        catch (MissingImageException e)
        {
            ModelState.AddModelError(nameof(request.ImageFile), e.Message);
            return View(request);
        }

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

        return View(_requestResponseMapper.Map(recipe));
    }

    // POST: Recipe/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, BLL_DTO.RecipeRequest request)
    {
        if (id != request.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await businessLogic.Recipes.UpdateAsync(request, Guid.Parse(userManager.GetUserId(User)!),
                    environment.WebRootPath);
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

        return View(request);
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
        await businessLogic.Recipes.RemoveAsync(id, environment.WebRootPath);
        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}