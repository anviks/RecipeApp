using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class PrizesController(AppDbContext context) : Controller
{
    // GET: Prizes
    public async Task<IActionResult> Index()
    {
        var appDbContext = context.Prizes.Include(p => p.Raffle).Include(p => p.RaffleResult);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Prizes/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prize = await context.Prizes
            .Include(p => p.Raffle)
            .Include(p => p.RaffleResult)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (prize == null)
        {
            return NotFound();
        }

        return View(prize);
    }

    // GET: Prizes/Create
    public IActionResult Create()
    {
        ViewData["RaffleId"] = new SelectList(context.Raffles, "Id", "RaffleName");
        ViewData["RaffleResultId"] = new SelectList(context.RaffleResults, "Id", "Id");
        return View();
    }

    // POST: Prizes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PrizeName,RaffleResultId,RaffleId,Id")] Prize prize)
    {
        if (ModelState.IsValid)
        {
            prize.Id = Guid.NewGuid();
            context.Add(prize);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["RaffleId"] = new SelectList(context.Raffles, "Id", "RaffleName", prize.RaffleId);
        ViewData["RaffleResultId"] = new SelectList(context.RaffleResults, "Id", "Id", prize.RaffleResultId);
        return View(prize);
    }

    // GET: Prizes/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prize = await context.Prizes.FindAsync(id);
        if (prize == null)
        {
            return NotFound();
        }
        ViewData["RaffleId"] = new SelectList(context.Raffles, "Id", "RaffleName", prize.RaffleId);
        ViewData["RaffleResultId"] = new SelectList(context.RaffleResults, "Id", "Id", prize.RaffleResultId);
        return View(prize);
    }

    // POST: Prizes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("PrizeName,RaffleResultId,RaffleId,Id")] Prize prize)
    {
        if (id != prize.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(prize);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrizeExists(prize.Id))
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
        ViewData["RaffleId"] = new SelectList(context.Raffles, "Id", "RaffleName", prize.RaffleId);
        ViewData["RaffleResultId"] = new SelectList(context.RaffleResults, "Id", "Id", prize.RaffleResultId);
        return View(prize);
    }

    // GET: Prizes/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prize = await context.Prizes
            .Include(p => p.Raffle)
            .Include(p => p.RaffleResult)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (prize == null)
        {
            return NotFound();
        }

        return View(prize);
    }

    // POST: Prizes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var prize = await context.Prizes.FindAsync(id);
        if (prize != null)
        {
            context.Prizes.Remove(prize);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PrizeExists(Guid id)
    {
        return context.Prizes.Any(e => e.Id == id);
    }
}