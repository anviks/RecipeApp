using App.DAL.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Infrastructure.Data.EntityFramework;
using RecipeApp.Infrastructure.Data.EntityFramework.Entities;

namespace RecipeApp.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class IngredientTypeAssociationsController(AppDbContext context) : Controller
{
    // GET: IngredientTypeAssociations
    public async Task<IActionResult> Index()
    {
        var appDbContext = context.IngredientTypeAssociations.Include(i => i.Ingredient).Include(i => i.IngredientType);
        return View(await appDbContext.ToListAsync());
    }

    // GET: IngredientTypeAssociations/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ingredientTypeAssociation = await context.IngredientTypeAssociations
            .Include(i => i.Ingredient)
            .Include(i => i.IngredientType)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ingredientTypeAssociation == null)
        {
            return NotFound();
        }

        return View(ingredientTypeAssociation);
    }

    // GET: IngredientTypeAssociations/Create
    public IActionResult Create()
    {
        ViewData["IngredientId"] = new SelectList(context.Ingredients, "Id", "Id");
        ViewData["IngredientTypeId"] = new SelectList(context.IngredientTypes, "Id", "Id");
        return View();
    }

    // POST: IngredientTypeAssociations/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IngredientId,IngredientTypeId,Id")] IngredientTypeAssociation ingredientTypeAssociation)
    {
        if (ModelState.IsValid)
        {
            ingredientTypeAssociation.Id = Guid.NewGuid();
            context.Add(ingredientTypeAssociation);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["IngredientId"] = new SelectList(context.Ingredients, "Id", "Id", ingredientTypeAssociation.IngredientId);
        ViewData["IngredientTypeId"] = new SelectList(context.IngredientTypes, "Id", "Id", ingredientTypeAssociation.IngredientTypeId);
        return View(ingredientTypeAssociation);
    }

    // GET: IngredientTypeAssociations/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ingredientTypeAssociation = await context.IngredientTypeAssociations.FindAsync(id);
        if (ingredientTypeAssociation == null)
        {
            return NotFound();
        }
        ViewData["IngredientId"] = new SelectList(context.Ingredients, "Id", "Id", ingredientTypeAssociation.IngredientId);
        ViewData["IngredientTypeId"] = new SelectList(context.IngredientTypes, "Id", "Id", ingredientTypeAssociation.IngredientTypeId);
        return View(ingredientTypeAssociation);
    }

    // POST: IngredientTypeAssociations/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("IngredientId,IngredientTypeId,Id")] IngredientTypeAssociation ingredientTypeAssociation)
    {
        if (id != ingredientTypeAssociation.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(ingredientTypeAssociation);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientTypeAssociationExists(ingredientTypeAssociation.Id))
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
        ViewData["IngredientId"] = new SelectList(context.Ingredients, "Id", "Id", ingredientTypeAssociation.IngredientId);
        ViewData["IngredientTypeId"] = new SelectList(context.IngredientTypes, "Id", "Id", ingredientTypeAssociation.IngredientTypeId);
        return View(ingredientTypeAssociation);
    }

    // GET: IngredientTypeAssociations/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ingredientTypeAssociation = await context.IngredientTypeAssociations
            .Include(i => i.Ingredient)
            .Include(i => i.IngredientType)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ingredientTypeAssociation == null)
        {
            return NotFound();
        }

        return View(ingredientTypeAssociation);
    }

    // POST: IngredientTypeAssociations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var ingredientTypeAssociation = await context.IngredientTypeAssociations.FindAsync(id);
        if (ingredientTypeAssociation != null)
        {
            context.IngredientTypeAssociations.Remove(ingredientTypeAssociation);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool IngredientTypeAssociationExists(Guid id)
    {
        return context.IngredientTypeAssociations.Any(e => e.Id == id);
    }
}