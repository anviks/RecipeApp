using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Contracts;
using RecipeApp.Application.DTO;
using RecipeApp.Base.Helpers;
using RecipeApp.Infrastructure.Data.EntityFramework.Entities.Identity;
using RecipeApp.Web.ViewModels;

namespace RecipeApp.Web.Controllers;

[Authorize]
public class ReviewsController(
    IAppBusinessLogic businessLogic, 
    UserManager<AppUser> userManager,
    IMapper mapper) : Controller
{
    private readonly EntityMapper<ReviewRequest, ReviewResponse> _mapper = new(mapper);
    
    // GET: Review
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var reviews = await businessLogic.Reviews.GetAllAsync();
        var reviewViewModels = new List<ReviewDetailsViewModel>();
        foreach (ReviewResponse review in reviews)
        {
            RecipeResponse? recipe = await businessLogic.Recipes.GetByIdAsync(review.RecipeId);
            reviewViewModels.Add(new ReviewDetailsViewModel
            {
                ReviewResponse = review,
                RecipeTitle = recipe!.Title
            });
        }

        return View(reviewViewModels);
    }

    // GET: Review/Details/5
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        ReviewResponse? review = await businessLogic.Reviews.GetByIdAsync(id.Value);
        if (review == null)
        {
            return NotFound();
        }

        RecipeResponse? recipe = await businessLogic.Recipes.GetByIdAsync(review.RecipeId);
        var reviewViewModel = new ReviewDetailsViewModel
        {
            ReviewResponse = review,
            RecipeTitle = recipe!.Title
        };

        return View(reviewViewModel);
    }

    // GET: Review/Create
    public async Task<IActionResult> Create()
    {
        var viewModel = new ReviewCreateEditViewModel
        {
            RecipeSelectList = new SelectList(await businessLogic.Recipes.GetAllAsync(), "Id", "Title")
        };
        return View(viewModel);
    }

    // POST: Review/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ReviewCreateEditViewModel viewModel)
    {
        ReviewRequest reviewRequest = viewModel.ReviewRequest;
        
        if (ModelState.IsValid)
        {
            reviewRequest.Id = Guid.NewGuid();
            businessLogic.Reviews.Add(reviewRequest, Guid.Parse(userManager.GetUserId(User)!));
            await businessLogic.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.RecipeSelectList = new SelectList(await businessLogic.Recipes.GetAllAsync(),
            nameof(RecipeResponse.Id),
            nameof(RecipeResponse.Title));
        
        return View(viewModel);
    }

    // GET: Review/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        ReviewResponse? review = await businessLogic.Reviews.GetByIdAsync(id.Value);
        if (review == null)
        {
            return NotFound();
        }

        var viewModel = new ReviewCreateEditViewModel
        {
            ReviewRequest = _mapper.Map(review)!,
            RecipeSelectList = new SelectList(await businessLogic.Recipes.GetAllAsync(), nameof(RecipeResponse.Id),
                nameof(RecipeResponse.Title))
        };
        
        return View(viewModel);
    }

    // POST: Review/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ReviewCreateEditViewModel viewModel)
    {
        ReviewRequest reviewRequest = viewModel.ReviewRequest;
        
        if (id != reviewRequest.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await businessLogic.Reviews.UpdateAsync(reviewRequest);
                await businessLogic.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await businessLogic.Reviews.ExistsAsync(reviewRequest.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        viewModel.RecipeSelectList = new SelectList(await businessLogic.Recipes.GetAllAsync(), nameof(RecipeResponse.Id),
            nameof(RecipeResponse.Title));
        
        return View(viewModel);
    }

    // GET: Review/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        ReviewResponse? review = await businessLogic.Reviews.GetByIdAsync(id.Value);
        if (review == null)
        {
            return NotFound();
        }
        
        RecipeResponse? recipe = await businessLogic.Recipes.GetByIdAsync(review.RecipeId);
        var reviewViewModel = new ReviewDetailsViewModel
        {
            ReviewResponse = review,
            RecipeTitle = recipe!.Title
        };

        return View(reviewViewModel);
    }

    // POST: Review/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        ReviewResponse? review = await businessLogic.Reviews.GetByIdAsync(id);
        if (review != null)
        {
            await businessLogic.Reviews.DeleteAsync(review);
        }

        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}