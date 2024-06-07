using App.DAL.EF;
using App.DAL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.Contracts;

namespace WebApp.Controllers;

public class ActivityTypesController(IAppUnitOfWork unitOfWork) : Controller
{
    // GET: ActivityTypes
    public async Task<IActionResult> Index()
    {
        return View(await unitOfWork.ActivityTypes.FindAllAsync());
    }

    // GET: ActivityTypes/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var activityType = await unitOfWork.ActivityTypes.FindAsync(id.Value);
        if (activityType == null)
        {
            return NotFound();
        }

        return View(activityType);
    }

    // GET: ActivityTypes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ActivityTypes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ActivityTypeName,Id")] ActivityType activityType)
    {
        if (ModelState.IsValid)
        {
            activityType.Id = Guid.NewGuid();
            unitOfWork.ActivityTypes.Add(activityType);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(activityType);
    }

    // GET: ActivityTypes/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var activityType = await unitOfWork.ActivityTypes.FindAsync(id.Value);
        if (activityType == null)
        {
            return NotFound();
        }
        return View(activityType);
    }

    // POST: ActivityTypes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("ActivityTypeName,Id")] ActivityType activityType)
    {
        if (id != activityType.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                unitOfWork.ActivityTypes.Update(activityType);
                await unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unitOfWork.ActivityTypes.ExistsAsync(activityType.Id))
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
        return View(activityType);
    }

    // GET: ActivityTypes/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var activityType = await unitOfWork.ActivityTypes.FindAsync(id.Value);
        if (activityType == null)
        {
            return NotFound();
        }

        return View(activityType);
    }

    // POST: ActivityTypes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var activityType = await unitOfWork.ActivityTypes.FindAsync(id);
        if (activityType != null)
        {
            await unitOfWork.ActivityTypes.RemoveAsync(activityType);
        }

        await unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}