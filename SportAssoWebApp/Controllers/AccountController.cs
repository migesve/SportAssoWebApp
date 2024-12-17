using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SportAssoWebApp.Models;
using Microsoft.EntityFrameworkCore;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SportAssoContext _context;

    public AccountController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        SportAssoContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context;
    }

    // POST: /Account/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password)
    {
        // Find user by email
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            // Check password
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if (result.Succeeded)
            {
                // Retrieve Adherent
                var adherent = await _context.Adherents
                    .FirstOrDefaultAsync(a => a.Email == email);

                // Add "IsValidated" claim if adherent is validated
                if (adherent != null && adherent.IsValidated)
                {
                    var existingClaims = await _userManager.GetClaimsAsync(user);
                    if (!existingClaims.Any(c => c.Type == "IsValidated"))
                    {
                        await _userManager.AddClaimAsync(user, new Claim("IsValidated", "true"));
                    }
                }

                return RedirectToAction("Index", "Home");
            }
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View();
    }
}
