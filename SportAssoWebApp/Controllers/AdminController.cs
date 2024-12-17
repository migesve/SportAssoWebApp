using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportAssoWebApp.Models;

namespace SportAssoWebApp.Controllers
{
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
    }
}
