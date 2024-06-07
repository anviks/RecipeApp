using App.DAL.EF;
using App.DAL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.Contracts;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class RafflesController(IAppUnitOfWork unitOfWork) : Controller
{
    // GET: Raffles
    public async Task<IActionResult> Index()
    {
        var raffles = await unitOfWork.Raffles.FindAllAsync();
        var viewModel = raffles.Select(r => new RaffleDetailsDeleteViewModel
        {
            Raffle = r,
            Company = unitOfWork.Companies.Find(r.CompanyId)!.CompanyName
        });
        return View(viewModel);
    }

    // GET: Raffles/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var raffle = await unitOfWork.Raffles.FindAsync(id.Value);
        if (raffle == null)
        {
            return NotFound();
        }

        var viewModel = new RaffleDetailsDeleteViewModel
        {
            Raffle = raffle,
            Company = (await unitOfWork.Companies.FindAsync(raffle.CompanyId))!.CompanyName
        };

        return View(viewModel);
    }

         // GET: Raffles/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new RaffleCreateEditViewModel
            {
                Companies = new SelectList(await unitOfWork.Companies.FindAllAsync(), "Id", "CompanyName")
            };
            return View(viewModel);
        }

        // POST: Raffles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RaffleCreateEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Raffle.Id = Guid.NewGuid();
                unitOfWork.Raffles.Add(viewModel.Raffle);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            viewModel.Companies = new SelectList(await unitOfWork.Companies.FindAllAsync(), "Id", "CompanyName", viewModel.Raffle.CompanyId);
            return View(viewModel);
        }

        // GET: Raffles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raffle = await unitOfWork.Raffles.FindAsync(id.Value);
            if (raffle == null)
            {
                return NotFound();
            }

            var viewModel = new RaffleCreateEditViewModel
            {
                Raffle = raffle,
                Companies = new SelectList(await unitOfWork.Companies.FindAllAsync(), "Id", "CompanyName", raffle.CompanyId)
            };
            return View(viewModel);
        }

        // POST: Raffles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RaffleCreateEditViewModel viewModel)
        {
            if (id != viewModel.Raffle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.Raffles.Update(viewModel.Raffle);
                    await unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await unitOfWork.Raffles.ExistsAsync(viewModel.Raffle.Id))
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
            viewModel.Companies = new SelectList(await unitOfWork.Companies.FindAllAsync(), "Id", "CompanyName", viewModel.Raffle.CompanyId);
            return View(viewModel);
        }

    // GET: Raffles/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var raffle = await unitOfWork.Raffles.FindAsync(id.Value);
        if (raffle == null)
        {
            return NotFound();
        }

        var viewModel = new RaffleDetailsDeleteViewModel
        {
            Raffle = raffle,
            Company = (await unitOfWork.Companies.FindAsync(raffle.CompanyId))!.CompanyName
        };

        return View(viewModel);
    }

    // POST: Raffles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var raffle = await unitOfWork.Raffles.FindAsync(id);
        if (raffle != null)
        {
            await unitOfWork.Raffles.RemoveAsync(raffle);
        }

        await unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}