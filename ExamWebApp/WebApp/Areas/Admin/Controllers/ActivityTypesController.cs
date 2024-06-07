using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ActivityTypesController(AppDbContext context) : Controller
{
    // GET: ActivityTypes
    public async Task<IActionResult> Index()
    {
        return View(await context.ActivityTypes.ToListAsync());
    }

    // GET: ActivityTypes/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var activityType = await context.ActivityTypes
            .FirstOrDefaultAsync(m => m.Id == id);
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
            context.Add(activityType);
            await context.SaveChangesAsync();
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

        var activityType = await context.ActivityTypes.FindAsync(id);
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
                context.Update(activityType);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityTypeExists(activityType.Id))
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

        var activityType = await context.ActivityTypes
            .FirstOrDefaultAsync(m => m.Id == id);
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
        var activityType = await context.ActivityTypes.FindAsync(id);
        if (activityType != null)
        {
            context.ActivityTypes.Remove(activityType);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ActivityTypeExists(Guid id)
    {
        return context.ActivityTypes.Any(e => e.Id == id);
    }
}