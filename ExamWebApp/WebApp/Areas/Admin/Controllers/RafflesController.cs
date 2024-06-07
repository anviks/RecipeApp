using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RafflesController(AppDbContext context) : Controller
{
    // GET: Raffles
    public async Task<IActionResult> Index()
    {
        var appDbContext = context.Raffles.Include(r => r.Company);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Raffles/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var raffle = await context.Raffles
            .Include(r => r.Company)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (raffle == null)
        {
            return NotFound();
        }

        return View(raffle);
    }

    // GET: Raffles/Create
    public IActionResult Create()
    {
        ViewData["CompanyId"] = new SelectList(context.Companies, "Id", "CompanyName");
        return View();
    }

    // POST: Raffles/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("RaffleName,VisibleToPublic,AllowAnonymousUsers,StartDate,EndDate,CompanyId,Id")] Raffle raffle)
    {
        if (ModelState.IsValid)
        {
            raffle.Id = Guid.NewGuid();
            context.Add(raffle);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CompanyId"] = new SelectList(context.Companies, "Id", "CompanyName", raffle.CompanyId);
        return View(raffle);
    }

    // GET: Raffles/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var raffle = await context.Raffles.FindAsync(id);
        if (raffle == null)
        {
            return NotFound();
        }
        ViewData["CompanyId"] = new SelectList(context.Companies, "Id", "CompanyName", raffle.CompanyId);
        return View(raffle);
    }

    // POST: Raffles/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("RaffleName,VisibleToPublic,AllowAnonymousUsers,StartDate,EndDate,CompanyId,Id")] Raffle raffle)
    {
        if (id != raffle.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(raffle);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaffleExists(raffle.Id))
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
        ViewData["CompanyId"] = new SelectList(context.Companies, "Id", "CompanyName", raffle.CompanyId);
        return View(raffle);
    }

    // GET: Raffles/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var raffle = await context.Raffles
            .Include(r => r.Company)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (raffle == null)
        {
            return NotFound();
        }

        return View(raffle);
    }

    // POST: Raffles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var raffle = await context.Raffles.FindAsync(id);
        if (raffle != null)
        {
            context.Raffles.Remove(raffle);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RaffleExists(Guid id)
    {
        return context.Raffles.Any(e => e.Id == id);
    }
}