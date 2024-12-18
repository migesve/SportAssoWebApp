using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace SportAssoWebApp.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAdminUser(IServiceProvider serviceProvider)
        {
            // Créer un scope pour utiliser les services
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // 1. Ajouter le rôle "Admin" s'il n'existe pas
            const string adminRole = "Admin";
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // 2. Ajouter un utilisateur administrateur s'il n'existe pas
            const string adminEmail = "admin@admin.com";
            const string adminPassword = "admin123"; // Remplacez par un mot de passe sécurisé

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                // Créer un nouvel utilisateur administrateur
                var newAdmin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true // Facultatif : Confirme automatiquement l'email
                };

                var result = await userManager.CreateAsync(newAdmin, adminPassword);
                if (result.Succeeded)
                {
                    // Assigner le rôle Admin
                    await userManager.AddToRoleAsync(newAdmin, adminRole);
                    Console.WriteLine("Utilisateur administrateur ajouté avec succès.");
                }
                else
                {
                    Console.WriteLine("Erreur lors de l'ajout de l'utilisateur administrateur :");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"- {error.Description}");
                    }
                }
            }
            else
            {
                Console.WriteLine("L'utilisateur administrateur existe déjà.");
            }
        }
    }
}