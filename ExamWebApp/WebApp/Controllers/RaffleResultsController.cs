using App.DAL.EF;
using App.DAL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.Contracts;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class RaffleResultsController(IAppUnitOfWork unitOfWork) : Controller
{
    // GET: RaffleResults
    public async Task<IActionResult> Index()
    {
        var raffleResults = await unitOfWork.RaffleResults.FindAllAsync();
        var prizes = await unitOfWork.Prizes.FindAllAsync();
        var viewModel = raffleResults.Select(r => new RaffleResultDetailsDeleteViewModel
        {
            RaffleResult = r,
            Raffle = unitOfWork.Raffles.Find(r.RaffleId)!.RaffleName,
            User = unitOfWork.Users.Find(r.UserId ?? Guid.Empty)?.UserName ?? "",
            Prize = prizes.FirstOrDefault(p => p.RaffleResultId == r.Id)?.PrizeName
        });
        return View(viewModel);
    }

    // GET: RaffleResults/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prizes = await unitOfWork.Prizes.FindAllAsync();
        var raffleResult = await unitOfWork.RaffleResults.FindAsync(id.Value);
        if (raffleResult == null)
        {
            return NotFound();
        }

        var viewModel = new RaffleResultDetailsDeleteViewModel
        {
            RaffleResult = raffleResult,
            Raffle = (await unitOfWork.Raffles.FindAsync(raffleResult.RaffleId))?.RaffleName!,
            User = (await unitOfWork.Users.FindAsync(raffleResult.UserId ?? Guid.Empty))?.Id.ToString() ?? "",
            Prize = prizes.FirstOrDefault(p => p.RaffleResultId == raffleResult.Id)?.PrizeName
        };

        return View(viewModel);
    }

    // GET: RaffleResults/Create
    public async Task<IActionResult> Create()
    {
        var viewModel = new RaffleResultCreateEditViewModel
        {
            Raffles = new SelectList(await unitOfWork.Raffles.FindAllAsync(), "Id", "RaffleName"),
            Users = new SelectList(await unitOfWork.Users.FindAllAsync(), "Id", "UserName")
        };
        return View(viewModel);
    }

    // POST: RaffleResults/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RaffleResultCreateEditViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            viewModel.RaffleResult.Id = Guid.NewGuid();
            unitOfWork.RaffleResults.Add(viewModel.RaffleResult);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.Raffles = new SelectList(await unitOfWork.Raffles.FindAllAsync(), "Id", "RaffleName",
            viewModel.RaffleResult.RaffleId);
        viewModel.Users =
            new SelectList(await unitOfWork.Users.FindAllAsync(), "Id", "UserName", viewModel.RaffleResult.UserId);
        return View(viewModel);
    }

    // GET: RaffleResults/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var raffleResult = await unitOfWork.RaffleResults.FindAsync(id.Value);
        if (raffleResult == null)
        {
            return NotFound();
        }

        var viewModel = new RaffleResultCreateEditViewModel
        {
            RaffleResult = raffleResult,
            Raffles =
                new SelectList(await unitOfWork.Raffles.FindAllAsync(), "Id", "RaffleName", raffleResult.RaffleId),
            Users = new SelectList(await unitOfWork.Users.FindAllAsync(), "Id", "UserName", raffleResult.UserId)
        };
        return View(viewModel);
    }

    // POST: RaffleResults/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, RaffleResultCreateEditViewModel viewModel)
    {
        if (id != viewModel.RaffleResult.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                unitOfWork.RaffleResults.Update(viewModel.RaffleResult);
                await unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unitOfWork.RaffleResults.ExistsAsync(viewModel.RaffleResult.Id))
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

        viewModel.Raffles = new SelectList(await unitOfWork.Raffles.FindAllAsync(), "Id", "RaffleName",
            viewModel.RaffleResult.RaffleId);
        viewModel.Users =
            new SelectList(await unitOfWork.Users.FindAllAsync(), "Id", "UserName", viewModel.RaffleResult.UserId);
        return View(viewModel);
    }

    // GET: RaffleResults/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var raffleResult = await unitOfWork.RaffleResults.FindAsync(id.Value);
        if (raffleResult == null)
        {
            return NotFound();
        }

        var prizes = await unitOfWork.Prizes.FindAllAsync();
        var viewModel = new RaffleResultDetailsDeleteViewModel
        {
            RaffleResult = raffleResult,
            Raffle = (await unitOfWork.Raffles.FindAsync(raffleResult.RaffleId))?.RaffleName!,
            User = (await unitOfWork.Users.FindAsync(raffleResult.UserId ?? Guid.Empty))?.Id.ToString() ?? "",
            Prize = prizes.FirstOrDefault(p => p.RaffleResultId == raffleResult.Id)?.PrizeName
        };

        return View(viewModel);
    }

    // POST: RaffleResults/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var raffleResult = await unitOfWork.RaffleResults.FindAsync(id);
        if (raffleResult != null)
        {
            await unitOfWork.RaffleResults.RemoveAsync(raffleResult);
        }

        await unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}