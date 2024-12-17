using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportAssoWebApp.Models;

namespace SportAssoWebApp.Controllers
{
    public class CreneauxController : Controller
    {
        private readonly SportAssoContext _context;

        public CreneauxController(SportAssoContext context)
        {
            _context = context;
        }

        // GET: Creneaux (Calendrier avec navigation par semaine)
        public async Task<IActionResult> Index(DateTime? weekStart)
        {
            // Récupération du lundi de la semaine en cours ou de la semaine spécifiée
            DateTime startOfWeek = weekStart?.StartOfWeek(DayOfWeek.Monday) ?? DateTime.Now.StartOfWeek(DayOfWeek.Monday);

            var creneaux = await _context.Creneaux
                .Include(c => c.Section)
                .Where(c => c.Date >= startOfWeek &&
                            c.Date < startOfWeek.AddDays(7))

                .ToListAsync();

            ViewBag.CurrentWeekStart = startOfWeek;
            return View(creneaux);

        }

        // GET: Creneaux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creneau = await _context.Creneaux
                .Include(c => c.Section)
                .FirstOrDefaultAsync(m => m.CreneauId == id);
            if (creneau == null)
            {
                return NotFound();
            }

            return View(creneau);
        }

        // GET: Creneaux/Create
        public IActionResult Create()
        {
            ViewData["SectionId"] = new SelectList(_context.Set<Section>(), "SectionId", "SectionId");
            return View();
        }

        // POST: Creneaux/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreneauId,SectionId,Lieu,Date,Hour,PlacesMax,PlacesRestantes,Price,DocumentsRequired")] Creneau creneau)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creneau);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SectionId"] = new SelectList(_context.Set<Section>(), "SectionId", "SectionId", creneau.SectionId);
            return View(creneau);
        }

        // GET: Creneaux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creneau = await _context.Creneaux.FindAsync(id);
            if (creneau == null)
            {
                return NotFound();
            }
            ViewData["SectionId"] = new SelectList(_context.Set<Section>(), "SectionId", "SectionId", creneau.SectionId);
            return View(creneau);
        }

        // POST: Creneaux/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CreneauId,SectionId,Lieu,Horaire,PlacesMax,PlacesRestantes,Price,DocumentsRequired")] Creneau creneau)
        {
            if (id != creneau.CreneauId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creneau);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreneauExists(creneau.CreneauId))
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
            ViewData["SectionId"] = new SelectList(_context.Set<Section>(), "SectionId", "SectionId", creneau.SectionId);
            return View(creneau);
        }

        // GET: Creneaux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creneau = await _context.Creneaux
                .Include(c => c.Section)
                .FirstOrDefaultAsync(m => m.CreneauId == id);
            if (creneau == null)
            {
                return NotFound();
            }

            return View(creneau);
        }

        // POST: Creneaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var creneau = await _context.Creneaux.FindAsync(id);
            if (creneau != null)
            {
                _context.Creneaux.Remove(creneau);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreneauExists(int id)
        {
            return _context.Creneaux.Any(e => e.CreneauId == id);
        }
    }

    // Méthode d'extension pour calculer le début de la semaine
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
