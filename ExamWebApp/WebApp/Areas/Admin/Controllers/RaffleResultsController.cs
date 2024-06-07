using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RaffleResultsController(AppDbContext context) : Controller
{
    // GET: RaffleResults
    public async Task<IActionResult> Index()
    {
        var appDbContext = context.RaffleResults.Include(r => r.Raffle).Include(r => r.User);
        return View(await appDbContext.ToListAsync());
    }

    // GET: RaffleResults/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var raffleResult = await context.RaffleResults
            .Include(r => r.Raffle)
            .Include(r => r.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (raffleResult == null)
        {
            return NotFound();
        }

        return View(raffleResult);
    }

    // GET: RaffleResults/Create
    public IActionResult Create()
    {
        ViewData["RaffleId"] = new SelectList(context.Raffles, "Id", "RaffleName");
        ViewData["UserId"] = new SelectList(context.Users, "Id", "Id");
        return View();
    }

    // POST: RaffleResults/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("RaffleId,UserId,AnonymousUserName,Id")] RaffleResult raffleResult)
    {
        if (ModelState.IsValid)
        {
            raffleResult.Id = Guid.NewGuid();
            context.Add(raffleResult);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["RaffleId"] = new SelectList(context.Raffles, "Id", "RaffleName", raffleResult.RaffleId);
        ViewData["UserId"] = new SelectList(context.Users, "Id", "Id", raffleResult.UserId);
        return View(raffleResult);
    }

    // GET: RaffleResults/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var raffleResult = await context.RaffleResults.FindAsync(id);
        if (raffleResult == null)
        {
            return NotFound();
        }
        ViewData["RaffleId"] = new SelectList(context.Raffles, "Id", "RaffleName", raffleResult.RaffleId);
        ViewData["UserId"] = new SelectList(context.Users, "Id", "Id", raffleResult.UserId);
        return View(raffleResult);
    }

    // POST: RaffleResults/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("RaffleId,UserId,AnonymousUserName,Id")] RaffleResult raffleResult)
    {
        if (id != raffleResult.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(raffleResult);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaffleResultExists(raffleResult.Id))
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
        ViewData["RaffleId"] = new SelectList(context.Raffles, "Id", "RaffleName", raffleResult.RaffleId);
        ViewData["UserId"] = new SelectList(context.Users, "Id", "Id", raffleResult.UserId);
        return View(raffleResult);
    }

    // GET: RaffleResults/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var raffleResult = await context.RaffleResults
            .Include(r => r.Raffle)
            .Include(r => r.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (raffleResult == null)
        {
            return NotFound();
        }

        return View(raffleResult);
    }

    // POST: RaffleResults/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var raffleResult = await context.RaffleResults.FindAsync(id);
        if (raffleResult != null)
        {
            context.RaffleResults.Remove(raffleResult);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RaffleResultExists(Guid id)
    {
        return context.RaffleResults.Any(e => e.Id == id);
    }
}