using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ActivitiesController(AppDbContext context) : Controller
{
    // GET: Activities
    public async Task<IActionResult> Index()
    {
        var appDbContext = context.Activities.Include(a => a.ActivityType).Include(a => a.User);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Activities/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var activity = await context.Activities
            .Include(a => a.ActivityType)
            .Include(a => a.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (activity == null)
        {
            return NotFound();
        }

        return View(activity);
    }

    // GET: Activities/Create
    public IActionResult Create()
    {
        ViewData["ActivityTypeId"] = new SelectList(context.ActivityTypes, "Id", "ActivityTypeName");
        ViewData["UserId"] = new SelectList(context.Users, "Id", "Id");
        return View();
    }

    // POST: Activities/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Duration,Date,UserId,ActivityTypeId,Id")] Activity activity)
    {
        if (ModelState.IsValid)
        {
            activity.Id = Guid.NewGuid();
            context.Add(activity);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ActivityTypeId"] = new SelectList(context.ActivityTypes, "Id", "ActivityTypeName", activity.ActivityTypeId);
        ViewData["UserId"] = new SelectList(context.Users, "Id", "Id", activity.UserId);
        return View(activity);
    }

    // GET: Activities/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var activity = await context.Activities.FindAsync(id);
        if (activity == null)
        {
            return NotFound();
        }
        ViewData["ActivityTypeId"] = new SelectList(context.ActivityTypes, "Id", "ActivityTypeName", activity.ActivityTypeId);
        ViewData["UserId"] = new SelectList(context.Users, "Id", "Id", activity.UserId);
        return View(activity);
    }

    // POST: Activities/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Duration,Date,UserId,ActivityTypeId,Id")] Activity activity)
    {
        if (id != activity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(activity);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(activity.Id))
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
        ViewData["ActivityTypeId"] = new SelectList(context.ActivityTypes, "Id", "ActivityTypeName", activity.ActivityTypeId);
        ViewData["UserId"] = new SelectList(context.Users, "Id", "Id", activity.UserId);
        return View(activity);
    }

    // GET: Activities/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var activity = await context.Activities
            .Include(a => a.ActivityType)
            .Include(a => a.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (activity == null)
        {
            return NotFound();
        }

        return View(activity);
    }

    // POST: Activities/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var activity = await context.Activities.FindAsync(id);
        if (activity != null)
        {
            context.Activities.Remove(activity);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ActivityExists(Guid id)
    {
        return context.Activities.Any(e => e.Id == id);
    }
}