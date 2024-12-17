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
        public DbSet<SportAssoWebApp.Models.Creneau> Creneau { get; set; } = default!;
        public DbSet<SportAssoWebApp.Models.Adherent> Adherent { get; set; } = default!;
        public DbSet<SportAssoWebApp.Models.Inscription> Inscription { get; set; } = default!;
    }
}
