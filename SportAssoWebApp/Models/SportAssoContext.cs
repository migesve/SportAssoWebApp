using Microsoft.EntityFrameworkCore;
using SportAssoWebApp.Models;

namespace SportAssoWebApp.Models
{
    public class SportAssoContext : DbContext
    {

        public SportAssoContext(DbContextOptions<SportAssoContext> options)
            : base(options)
        {
        }

        // Parameterless constructor for design-time
        public SportAssoContext()
        {
        }
        public DbSet<SportAssoWebApp.Models.Creneau> Creneaux { get; set; } = default!;
        public DbSet<SportAssoWebApp.Models.Adherent> Adherents { get; set; } = default!;
        public DbSet<SportAssoWebApp.Models.Inscription> Inscriptions { get; set; } = default!;

        public DbSet<SportAssoWebApp.Models.Section> Sections { get; set; } = default!;

    }
}
