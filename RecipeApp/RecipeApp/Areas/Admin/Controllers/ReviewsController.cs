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
public class ReviewsController(IAppBusinessLogic businessLogic) : Controller
{
    // GET: Review
    public async Task<IActionResult> Index()
    {
        var reviews = await businessLogic.Reviews.FindAllAsync();
        var reviewViewModels = new List<ReviewDetailsViewModel>();
        foreach (Review review in reviews)
        {
            RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(review.RecipeId);
            reviewViewModels.Add(new ReviewDetailsViewModel
            {
                Review = review,
                RecipeTitle = recipe!.Title
            });
        }

        return View(reviewViewModels);
    }

    // GET: Review/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Review? review = await businessLogic.Reviews.FindAsync(id.Value);
        if (review == null)
        {
            return NotFound();
        }

        RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(review.RecipeId);
        var reviewViewModel = new ReviewDetailsViewModel
        {
            Review = review,
            RecipeTitle = recipe!.Title
        };

        return View(reviewViewModel);
    }

    // GET: Review/Create
    public async Task<IActionResult> Create()
    {
        var viewModel = new ReviewCreateEditViewModel
        {
            RecipeSelectList = new SelectList(await businessLogic.Recipes.FindAllAsync(), "Id", "Title")
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
        Review review = viewModel.Review;
        
        if (ModelState.IsValid)
        {
            review.Id = Guid.NewGuid();
            businessLogic.Reviews.Add(review);
            await businessLogic.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.RecipeSelectList = new SelectList(await businessLogic.Recipes.FindAllAsync(),
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

        Review? review = await businessLogic.Reviews.FindAsync(id.Value);
        if (review == null)
        {
            return NotFound();
        }

        // ViewData["RecipeId"] =
        //     new SelectList(await businessLogic.Recipes.FindAllAsync(), "Id", "Description", review.RecipeId);
        var viewModel = new ReviewCreateEditViewModel
        {
            Review = review,
            RecipeSelectList = new SelectList(await businessLogic.Recipes.FindAllAsync(), nameof(RecipeResponse.Id),
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
        Review review = viewModel.Review;
        
        if (id != review.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                businessLogic.Reviews.Update(review);
                await businessLogic.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await businessLogic.Reviews.ExistsAsync(review.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        viewModel.RecipeSelectList = new SelectList(await businessLogic.Recipes.FindAllAsync(), nameof(RecipeResponse.Id),
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

        Review? review = await businessLogic.Reviews.FindAsync(id.Value);
        if (review == null)
        {
            return NotFound();
        }
        
        RecipeResponse? recipe = await businessLogic.Recipes.FindAsync(review.RecipeId);
        var reviewViewModel = new ReviewDetailsViewModel
        {
            Review = review,
            RecipeTitle = recipe!.Title
        };

        return View(reviewViewModel);
    }

    // POST: Review/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        Review? review = await businessLogic.Reviews.FindAsync(id);
        if (review != null)
        {
            await businessLogic.Reviews.RemoveAsync(review);
        }

        await businessLogic.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}