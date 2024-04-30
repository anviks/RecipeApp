using App.Contracts.DAL;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ReviewsController(IAppUnitOfWork unitOfWork) : Controller
{
    // GET: Review
    public async Task<IActionResult> Index()
    {
        return View(await unitOfWork.Reviews.FindAllAsync());
    }

    // GET: Review/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Review? review = await unitOfWork.Reviews.FindAsync(id.Value);
        if (review == null)
        {
            return NotFound();
        }

        return View(review);
    }

    // GET: Review/Create
    public IActionResult Create()
    {
        ViewData["RecipeId"] = new SelectList(unitOfWork.Recipes.FindAll(), "Id", "Description");
        ViewData["UserId"] = new SelectList(unitOfWork.Users.FindAll(), "Id", "FirstName");
        return View();
    }

    // POST: Review/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("UserId,RecipeId,Rating,Content,CreatedAt,Id")] Review review)
    {
        if (ModelState.IsValid)
        {
            review.Id = Guid.NewGuid();
            unitOfWork.Reviews.Add(review);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["RecipeId"] = new SelectList(await unitOfWork.Recipes.FindAllAsync(), "Id", "Description", review.RecipeId);
        ViewData["UserId"] = new SelectList(await unitOfWork.Users.FindAllAsync(), "Id", "FirstName", review.UserId);
        return View(review);
    }

    // GET: Review/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Review? review = await unitOfWork.Reviews.FindAsync(id.Value);
        if (review == null)
        {
            return NotFound();
        }
        ViewData["RecipeId"] = new SelectList(await unitOfWork.Recipes.FindAllAsync(), "Id", "Description", review.RecipeId);
        ViewData["UserId"] = new SelectList(await unitOfWork.Users.FindAllAsync(), "Id", "FirstName", review.UserId);
        return View(review);
    }

    // POST: Review/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("UserId,RecipeId,Rating,Content,CreatedAt,Id")] Review review)
    {
        if (id != review.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                unitOfWork.Reviews.Update(review);
                await unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unitOfWork.Reviews.ExistsAsync(review.Id))
                {
                    return NotFound();
                }

                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["RecipeId"] = new SelectList(await unitOfWork.Recipes.FindAllAsync(), "Id", "Description", review.RecipeId);
        ViewData["UserId"] = new SelectList(await unitOfWork.Users.FindAllAsync(), "Id", "FirstName", review.UserId);
        return View(review);
    }

    // GET: Review/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Review? review = await unitOfWork.Reviews.FindAsync(id.Value);
        if (review == null)
        {
            return NotFound();
        }

        return View(review);
    }

    // POST: Review/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        Review? review = await unitOfWork.Reviews.FindAsync(id);
        if (review != null)
        {
            await unitOfWork.Reviews.RemoveAsync(review);
        }

        await unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}