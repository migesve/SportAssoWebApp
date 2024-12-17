using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportAssoWebApp.Models;

public class SportsController : Controller
{
    private readonly SportAssoContext _context;

    public SportsController(SportAssoContext context)
    {
        _context = context;
    }

    // Display available sports sections
    public async Task<IActionResult> Index()
    {
        if (!User.HasClaim(c => c.Type == "IsValidated" && c.Value == "true"))
        {
            return RedirectToAction("Index", "Home");
        }

        var sections = await _context.Sections.ToListAsync();
        return View(sections);
    }

    // Display creneaux for a specific section
    public async Task<IActionResult> Creneaux(int id)
    {
        var isValidated = User.Claims.Any(c => c.Type == "IsValidated" && c.Value == "true");
        if (!isValidated)
        {
            return RedirectToAction("Index", "Home");
        }

        var creneaux = await _context.Creneaux
            .Where(c => c.SectionId == id && c.PlacesRestantes > 0)
            .ToListAsync();
        return View(creneaux);
    }

    // Register in a creneau
    public async Task<IActionResult> Register(int id)
    {
        var creneau = await _context.Creneaux.FindAsync(id);
        if (creneau != null && creneau.PlacesRestantes > 0)
        {
            creneau.PlacesRestantes--;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}