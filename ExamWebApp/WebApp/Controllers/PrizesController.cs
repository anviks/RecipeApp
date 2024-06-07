using App.DAL.EF;
using App.DAL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.Contracts;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class PrizesController(IAppUnitOfWork unitOfWork) : Controller
{
    // GET: Prizes
    // GET: Prizes/Index
    public async Task<IActionResult> Index()
    {
        var prizes = await unitOfWork.Prizes.FindAllAsync();
        var viewModel = prizes.Select(p => new PrizeDetailsDeleteViewModel
        {
            Prize = p,
            Raffle = unitOfWork.Raffles.Find(p.RaffleId)!.RaffleName,
            RaffleResult = unitOfWork.RaffleResults.Find(p.RaffleResultId!.Value)!.Id.ToString()
        });
        return View(viewModel);
    }

    // GET: Prizes/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prize = await unitOfWork.Prizes.FindAsync(id.Value);
        if (prize == null)
        {
            return NotFound();
        }

        var viewModel = new PrizeDetailsDeleteViewModel
        {
            Prize = prize,
            Raffle = (await unitOfWork.Raffles.FindAsync(prize.RaffleId))!.RaffleName,
            RaffleResult = (await unitOfWork.RaffleResults.FindAsync(prize.RaffleResultId!.Value))!.Id.ToString()
        };
        
        return View(viewModel);
    }

    // GET: Prizes/Create
    public async Task<IActionResult> Create()
    {
        var viewModel = new PrizeCreateEditViewModel
        {
            Raffles = new SelectList(await unitOfWork.Raffles.FindAllAsync(), "Id", "RaffleName"),
            RaffleResults = new SelectList(await unitOfWork.RaffleResults.FindAllAsync(), "Id", "Id")
        };
        return View(viewModel);
    }

    // POST: Prizes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PrizeCreateEditViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            viewModel.Prize.Id = Guid.NewGuid();
            unitOfWork.Prizes.Add(viewModel.Prize);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        viewModel.Raffles = new SelectList(await unitOfWork.Raffles.FindAllAsync(), "Id", "RaffleName", viewModel.Prize.RaffleId);
        viewModel.RaffleResults = new SelectList(await unitOfWork.RaffleResults.FindAllAsync(), "Id", "Id", viewModel.Prize.RaffleResultId);
        return View(viewModel);
    }

    // GET: Prizes/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prize = await unitOfWork.Prizes.FindAsync(id.Value);
        if (prize == null)
        {
            return NotFound();
        }

        var viewModel = new PrizeCreateEditViewModel
        {
            Prize = prize,
            Raffles = new SelectList(await unitOfWork.Raffles.FindAllAsync(), "Id", "RaffleName", prize.RaffleId),
            RaffleResults = new SelectList(await unitOfWork.RaffleResults.FindAllAsync(), "Id", "Id", prize.RaffleResultId)
        };
        return View(viewModel);
    }

    // POST: Prizes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, PrizeCreateEditViewModel viewModel)
    {
        if (id != viewModel.Prize.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                unitOfWork.Prizes.Update(viewModel.Prize);
                await unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unitOfWork.Prizes.ExistsAsync(viewModel.Prize.Id))
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
        viewModel.Raffles = new SelectList(await unitOfWork.Raffles.FindAllAsync(), "Id", "RaffleName", viewModel.Prize.RaffleId);
        viewModel.RaffleResults = new SelectList(await unitOfWork.RaffleResults.FindAllAsync(), "Id", "Id", viewModel.Prize.RaffleResultId);
        return View(viewModel);
    }

    // GET: Prizes/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prize = await unitOfWork.Prizes.FindAsync(id.Value);
        if (prize == null)
        {
            return NotFound();
        }

        var viewModel = new PrizeDetailsDeleteViewModel
        {
            Prize = prize,
            Raffle = (await unitOfWork.Raffles.FindAsync(prize.RaffleId))!.RaffleName,
            RaffleResult = (await unitOfWork.RaffleResults.FindAsync(prize.RaffleResultId!.Value))!.Id.ToString()
        };
        
        return View(viewModel);
    }

    // POST: Prizes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var prize = await unitOfWork.Prizes.FindAsync(id);
        if (prize != null)
        {
            await unitOfWork.Prizes.RemoveAsync(prize);
        }

        await unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}