using App.DAL.EF;
using App.DAL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.Contracts;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize]
public class TicketsController(IAppUnitOfWork unitOfWork, UserManager<AppUser> userManager) : Controller
{
    // GET: Tickets
    public async Task<IActionResult> Index()
    {
        var tickets = await unitOfWork.Tickets.FindAllAsync();
        return View(tickets.Select(t => new TicketDetailsDeleteViewModel
        {
            Ticket = t,
            User = unitOfWork.Users.Find(t.UserId)!.UserName!,
            Raffle = unitOfWork.Raffles.Find(t.RaffleId)!.RaffleName
        }));
    }

    // GET: Tickets/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket = await unitOfWork.Tickets.FindAsync(id.Value);
        if (ticket == null)
        {
            return NotFound();
        }

        var viewModel = new TicketDetailsDeleteViewModel
        {
            Ticket = ticket,
            User = (await unitOfWork.Users.FindAsync(ticket.UserId))!.UserName!,
            Raffle = (await unitOfWork.Raffles.FindAsync(ticket.RaffleId))!.RaffleName
        };

        return View(viewModel);
    }

    // GET: Tickets/Create
    public async Task<IActionResult> Create()
    {
        var viewModel = new TicketCreateEditViewModel
        {
            Users = new SelectList(await unitOfWork.Users.FindAllAsync(), "Id", "UserName"),
            Raffles = new SelectList(await unitOfWork.Raffles.FindAllAsync(), "Id", "RaffleName")
        };
        return View(viewModel);
    }

    // POST: Tickets/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TicketCreateEditViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            viewModel.Ticket.Id = Guid.NewGuid();
            unitOfWork.Tickets.Add(viewModel.Ticket);

            var results = await unitOfWork.RaffleResults.FindAllAsync();
            foreach (RaffleResult result in results)
            {
                if (result.UserId == viewModel.Ticket.UserId) goto skipResultCreation;
            }
            unitOfWork.RaffleResults.Add(new RaffleResult
            {
                Id = Guid.NewGuid(),
                UserId = viewModel.Ticket.UserId,
                RaffleId = viewModel.Ticket.RaffleId
            });
            skipResultCreation:
            
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.Users = new SelectList(await unitOfWork.Users.FindAllAsync(), "Id", "UserName", viewModel.Ticket.UserId);
        viewModel.Raffles = new SelectList(await unitOfWork.Raffles.FindAllAsync(), "Id", "RaffleName",
            viewModel.Ticket.RaffleId);
        return View(viewModel);
    }

    // GET: Tickets/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket = await unitOfWork.Tickets.FindAsync(id.Value);
        if (ticket == null)
        {
            return NotFound();
        }

        var viewModel = new TicketCreateEditViewModel
        {
            Ticket = ticket,
            Users = new SelectList(await unitOfWork.Users.FindAllAsync(), "Id", "UserName", ticket.UserId),
            Raffles = new SelectList(await unitOfWork.Raffles.FindAllAsync(), "Id", "RaffleName", ticket.RaffleId)
        };
        return View(viewModel);
    }

    // POST: Tickets/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, TicketCreateEditViewModel viewModel)
    {
        if (id != viewModel.Ticket.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                unitOfWork.Tickets.Update(viewModel.Ticket);
                await unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unitOfWork.Tickets.ExistsAsync(viewModel.Ticket.Id))
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

        viewModel.Users = new SelectList(await unitOfWork.Users.FindAllAsync(), "Id", "UserName", viewModel.Ticket.UserId);
        viewModel.Raffles = new SelectList(await unitOfWork.Raffles.FindAllAsync(), "Id", "RaffleName",
            viewModel.Ticket.RaffleId);
        return View(viewModel);
    }

    // GET: Tickets/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket = await unitOfWork.Tickets.FindAsync(id.Value);
        if (ticket == null)
        {
            return NotFound();
        }

        var viewModel = new TicketDetailsDeleteViewModel
        {
            Ticket = ticket,
            User = (await unitOfWork.Users.FindAsync(ticket.UserId))!.UserName!,
            Raffle = (await unitOfWork.Raffles.FindAsync(ticket.RaffleId))!.RaffleName
        };

        return View(viewModel);
    }

    // POST: Tickets/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var ticket = await unitOfWork.Tickets.FindAsync(id);
        if (ticket != null)
        {
            await unitOfWork.Tickets.RemoveAsync(ticket);
        }

        await unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}