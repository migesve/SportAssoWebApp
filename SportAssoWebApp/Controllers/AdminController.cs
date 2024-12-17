using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportAssoWebApp.Models;

namespace SportAssoWebApp.Controllers
{
    [Authorize(Roles = "Admin")] // Protect the controller for Admin role only
    public class AdminController : Controller
    {
        private readonly SportAssoContext _context;

        public AdminController(SportAssoContext context)
        {
            _context = context;
        }

        // GET: Admin/ValidateAdherents
        public async Task<IActionResult> ValidateAdherents()
        {
            // Fetch adherents that are not yet validated
            var adherentsToValidate = await _context.Adherents
                .Where(a => a.IsValidated == false)
                .ToListAsync();

            return View(adherentsToValidate);
        }

        // POST: Admin/Approve/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var adherent = await _context.Adherents.FindAsync(id);
            if (adherent == null)
            {
                return NotFound();
            }

            // Mark the adherent as validated
            adherent.IsValidated = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ValidateAdherents));
        }

        // GET: Admin/AddCreneau
        public IActionResult AddCreneau()
        {
            return View();
        }

        // POST: Admin/AddCreneau
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCreneau(Creneau creneau)
        {
            if (ModelState.IsValid)
            {
                _context.Creneaux.Add(creneau);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Creneaux");
            }
            return View(creneau);
        }
    }
}
