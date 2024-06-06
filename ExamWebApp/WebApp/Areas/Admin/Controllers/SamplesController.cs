using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SamplesController(AppDbContext context) : Controller
{
    // GET: Samples
    public async Task<IActionResult> Index()
    {
        return View(await context.Samples.ToListAsync());
    }

    // GET: Samples/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sample = await context.Samples
            .FirstOrDefaultAsync(m => m.Id == id);
        if (sample == null)
        {
            return NotFound();
        }

        return View(sample);
    }

    // GET: Samples/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Samples/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Field,Id")] Sample sample)
    {
        if (ModelState.IsValid)
        {
            sample.Id = Guid.NewGuid();
            context.Add(sample);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(sample);
    }

    // GET: Samples/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sample = await context.Samples.FindAsync(id);
        if (sample == null)
        {
            return NotFound();
        }
        return View(sample);
    }

    // POST: Samples/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Field,Id")] Sample sample)
    {
        if (id != sample.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(sample);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SampleExists(sample.Id))
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
        return View(sample);
    }

    // GET: Samples/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sample = await context.Samples
            .FirstOrDefaultAsync(m => m.Id == id);
        if (sample == null)
        {
            return NotFound();
        }

        return View(sample);
    }

    // POST: Samples/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var sample = await context.Samples.FindAsync(id);
        if (sample != null)
        {
            context.Samples.Remove(sample);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SampleExists(Guid id)
    {
        return context.Samples.Any(e => e.Id == id);
    }
}