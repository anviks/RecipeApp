using App.DAL.EF;
using App.DAL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.Contracts;

namespace WebApp.Controllers;

public class CompaniesController(IAppUnitOfWork unitOfWork) : Controller
{
    // GET: Companies
    public async Task<IActionResult> Index()
    {
        return View(await unitOfWork.Companies.FindAllAsync());
    }

    // GET: Companies/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var company = await unitOfWork.Companies.FindAsync(id.Value);
        if (company == null)
        {
            return NotFound();
        }

        return View(company);
    }

    // GET: Companies/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Companies/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CompanyName,Id")] Company company)
    {
        if (ModelState.IsValid)
        {
            company.Id = Guid.NewGuid();
            unitOfWork.Companies.Add(company);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(company);
    }

    // GET: Companies/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var company = await unitOfWork.Companies.FindAsync(id.Value);
        if (company == null)
        {
            return NotFound();
        }
        return View(company);
    }

    // POST: Companies/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("CompanyName,Id")] Company company)
    {
        if (id != company.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                unitOfWork.Companies.Update(company);
                await unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unitOfWork.Companies.ExistsAsync(company.Id))
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
        return View(company);
    }

    // GET: Companies/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var company = await unitOfWork.Companies.FindAsync(id.Value);
        if (company == null)
        {
            return NotFound();
        }

        return View(company);
    }

    // POST: Companies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var company = await unitOfWork.Companies.FindAsync(id);
        if (company != null)
        {
            await unitOfWork.Companies.RemoveAsync(company);
        }

        await unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}