using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Contracts;
using RecipeApp.Application.DTO;
using RecipeApp.Application.Exceptions;
using RecipeApp.Base.Helpers;
using RecipeApp.Infrastructure.Data.DTO;
using RecipeApp.Infrastructure.Data.EntityFramework.Entities.Identity;
using RecipeApp.Web.ViewModels;

namespace RecipeApp.Web.Controllers;

[Authorize]
public class RecipesController(
    IAppBusinessLogic businessLogic,
    IMapper mapper,
    UserManager<AppUser> userManager,
    IWebHostEnvironment environment
) : Controller
{
    private readonly EntityMapper<RecipeRequest, Recipe> _requestDalMapper = new(mapper);
    private readonly EntityMapper<RecipeRequest, RecipeResponse> _requestResponseMapper = new(mapper);

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

        RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(id.Value);

        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // GET: Recipe/Create
    public IActionResult Create()
    {
        return View(CreateViewModel());
    }

    // POST: Recipe/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RecipeCreateEditViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);
        
        var request = viewModel.RecipeRequest;

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
            return View(viewModel);
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

        RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(id.Value);
        if (recipe == null)
        {
            return NotFound();
        }
        
        var viewModel = new RecipeCreateEditViewModel
        {
            RecipeRequest = _requestResponseMapper.Map(recipe)!,
            // InstructionCount = recipe.Instructions.Count
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
        var request = viewModel.RecipeRequest;
        
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

        return View(viewModel);
    }

    // GET: Recipe/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(id.Value);
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

    [HttpPost]
    public IActionResult AddField(RecipeCreateEditViewModel viewModel)
    {
        viewModel.RecipeRequest.Instructions.Add("");
        return PartialView("_InstructionField", viewModel);
    }
    
    [HttpPost]
    public IActionResult RemoveField(RecipeCreateEditViewModel viewModel)
    {
        viewModel.RecipeRequest.Instructions.RemoveAt(viewModel.RecipeRequest.Instructions.Count - 1);
        return PartialView("_InstructionField", viewModel);
    }
    
    private static RecipeCreateEditViewModel CreateViewModel()
    {
        return new RecipeCreateEditViewModel
        {
            RecipeRequest = new RecipeRequest
            {
                Instructions = [""]
            },
            // InstructionCount = 1
        };
    }
}