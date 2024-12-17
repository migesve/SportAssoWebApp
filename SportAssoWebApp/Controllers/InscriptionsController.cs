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
    public class InscriptionsController : Controller
    {
        private readonly SportAssoContext _context;

        public InscriptionsController(SportAssoContext context)
        {
            _context = context;
        }

        // GET: Inscriptions
        public async Task<IActionResult> Index()
        {
            var sportAssoContext = _context.Inscriptions.Include(i => i.Adherent).Include(i => i.Creneau);
            return View(await sportAssoContext.ToListAsync());
        }

        // GET: Inscriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscription = await _context.Inscriptions
                .Include(i => i.Adherent)
                .Include(i => i.Creneau)
                .FirstOrDefaultAsync(m => m.InscriptionId == id);
            if (inscription == null)
            {
                return NotFound();
            }

            return View(inscription);
        }

        // GET: Inscriptions/Create
        public IActionResult Create()
        {
            ViewData["AdherentId"] = new SelectList(_context.Adherents, "AdherentId", "AdherentId");
            ViewData["CreneauId"] = new SelectList(_context.Creneaux, "CreneauId", "CreneauId");
            return View();
        }

        // POST: Inscriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InscriptionId,AdherentId,CreneauId,IsPaid,DocumentsSubmitted")] Inscription inscription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inscription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdherentId"] = new SelectList(_context.Adherents, "AdherentId", "AdherentId", inscription.AdherentId);
            ViewData["CreneauId"] = new SelectList(_context.Creneaux, "CreneauId", "CreneauId", inscription.CreneauId);
            return View(inscription);
        }

        // GET: Inscriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscription = await _context.Inscriptions.FindAsync(id);
            if (inscription == null)
            {
                return NotFound();
            }
            ViewData["AdherentId"] = new SelectList(_context.Adherents, "AdherentId", "AdherentId", inscription.AdherentId);
            ViewData["CreneauId"] = new SelectList(_context.Creneaux, "CreneauId", "CreneauId", inscription.CreneauId);
            return View(inscription);
        }

        // POST: Inscriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InscriptionId,AdherentId,CreneauId,IsPaid,DocumentsSubmitted")] Inscription inscription)
        {
            if (id != inscription.InscriptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscriptionExists(inscription.InscriptionId))
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
            ViewData["AdherentId"] = new SelectList(_context.Adherents, "AdherentId", "AdherentId", inscription.AdherentId);
            ViewData["CreneauId"] = new SelectList(_context.Creneaux, "CreneauId", "CreneauId", inscription.CreneauId);
            return View(inscription);
        }

        // GET: Inscriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscription = await _context.Inscriptions
                .Include(i => i.Adherent)
                .Include(i => i.Creneau)
                .FirstOrDefaultAsync(m => m.InscriptionId == id);
            if (inscription == null)
            {
                return NotFound();
            }

            return View(inscription);
        }

        // POST: Inscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inscription = await _context.Inscriptions.FindAsync(id);
            if (inscription != null)
            {
                _context.Inscriptions.Remove(inscription);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscriptionExists(int id)
        {
            return _context.Inscriptions.Any(e => e.InscriptionId == id);
        }
    }
}
