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

        // GET: Creneaux
        public async Task<IActionResult> Index()
        {
            var sportAssoContext = _context.Creneaux.Include(c => c.Section);
            return View(await sportAssoContext.ToListAsync());
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreneauId,SectionId,Lieu,Horaire,PlacesMax,PlacesRestantes,Price,DocumentsRequired")] Creneau creneau)
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
}
