using App.DAL.EF;
using App.DAL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.Contracts;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class ActivitiesController(IAppUnitOfWork unitOfWork) : Controller
{
    // GET: Activities
    public async Task<IActionResult> Index()
    {
        var activities = await unitOfWork.Activities.FindAllAsync();
        var viewModel = activities.Select(a => new ActivityDetailsDeleteViewModel
        {
            Activity = a,
            ActivityType = unitOfWork.ActivityTypes.Find(a.ActivityTypeId)!.ActivityTypeName,
            User = unitOfWork.Users.Find(a.UserId)!.UserName?.ToString() ?? ""
        });
        return View(viewModel);
    }

    // GET: Activities/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var activity = await unitOfWork.Activities.FindAsync(id.Value);
        if (activity == null)
        {
            return NotFound();
        }

        var viewModel = new ActivityDetailsDeleteViewModel
        {
            Activity = activity,
            ActivityType = (await unitOfWork.ActivityTypes.FindAsync(activity.ActivityTypeId))!.ActivityTypeName,
            User = (await unitOfWork.Users.FindAsync(activity.UserId))!.Id.ToString()
        };
        
        return View(viewModel);
    }

    // GET: Activities/Create
    public async Task<IActionResult> Create()
    {
        var viewModel = new ActivityCreateEditViewModel
        {
            ActivityTypes = new SelectList(await unitOfWork.ActivityTypes.FindAllAsync(), "Id", "ActivityTypeName"),
            Users = new SelectList(await unitOfWork.Users.FindAllAsync(), "Id", "UserName")
        };
        
        return View(viewModel);
    }

    // POST: Activities/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ActivityCreateEditViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            viewModel.Activity.Id = Guid.NewGuid();
            unitOfWork.Activities.Add(viewModel.Activity);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        viewModel.ActivityTypes = new SelectList(await unitOfWork.ActivityTypes.FindAllAsync(), "Id", "ActivityTypeName", viewModel.Activity.ActivityTypeId);
        viewModel.Users = new SelectList(await unitOfWork.Users.FindAllAsync(), "Id", "UserName", viewModel.Activity.UserId);
        return View(viewModel);
    }

    // GET: Activities/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var activity = await unitOfWork.Activities.FindAsync(id.Value);
        if (activity == null)
        {
            return NotFound();
        }
        var viewModel = new ActivityCreateEditViewModel
        {
            Activity = activity,
            ActivityTypes = new SelectList(await unitOfWork.ActivityTypes.FindAllAsync(), "Id", "ActivityTypeName", activity.ActivityTypeId),
            Users = new SelectList(await unitOfWork.Users.FindAllAsync(), "Id", "UserName", activity.UserId)
        };
        return View(viewModel);
    }

    // POST: Activities/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ActivityCreateEditViewModel viewModel)
    {
        if (id != viewModel.Activity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                unitOfWork.Activities.Update(viewModel.Activity);
                await unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unitOfWork.Activities.ExistsAsync(viewModel.Activity.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        viewModel.ActivityTypes = new SelectList(await unitOfWork.ActivityTypes.FindAllAsync(), "Id", "ActivityTypeName", viewModel.Activity.ActivityTypeId);
        viewModel.Users = new SelectList(await unitOfWork.Users.FindAllAsync(), "Id", "UserName", viewModel.Activity.UserId);
        return View(viewModel);
    }

    // GET: Activities/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var activity = await unitOfWork.Activities.FindAsync(id.Value);
        if (activity == null)
        {
            return NotFound();
        }

        var viewModel = new ActivityDetailsDeleteViewModel
        {
            Activity = activity,
            ActivityType = (await unitOfWork.ActivityTypes.FindAsync(activity.ActivityTypeId))!.ActivityTypeName,
            User = (await unitOfWork.Users.FindAsync(activity.UserId))!.Id.ToString()
        };
        
        return View(viewModel);
    }

    // POST: Activities/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var activity = await unitOfWork.Activities.FindAsync(id);
        if (activity != null)
        {
            await unitOfWork.Activities.RemoveAsync(activity);
        }

        await unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}