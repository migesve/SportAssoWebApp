using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportAssoWebApp.Models
{
    public class SportAssoContext : IdentityDbContext<IdentityUser>
    {
        public SportAssoContext(DbContextOptions<SportAssoContext> options)
            : base(options)
        {
        }

        // Application Data Tables
        public DbSet<Adherent> Adherents { get; set; }
        public DbSet<Creneau> Creneaux { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }
        public DbSet<Section> Sections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Required for Identity

            // Adherent Entity Configuration
            modelBuilder.Entity<Adherent>(entity =>
            {
                entity.HasKey(e => e.AdherentId);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.PasswordHash).HasMaxLength(256);
                entity.Property(e => e.IsEncadrant).HasDefaultValue(false);
                entity.Property(e => e.IsValidated).HasDefaultValue(false);
            });

            // Creneau Entity Configuration
            modelBuilder.Entity<Creneau>(entity =>
            {
                entity.HasKey(e => e.CreneauId);
                entity.ToTable("Creneaux");

                entity.Property(e => e.Date).HasColumnType("date");
                entity.Property(e => e.Hour).HasColumnType("time");
                entity.Property(e => e.Lieu).HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Section)
                      .WithMany(p => p.Creneaux)
                      .HasForeignKey(d => d.SectionId)
                      .HasConstraintName("FK_Creneaux_Section");
            });

            // Inscription Entity Configuration
            modelBuilder.Entity<Inscription>(entity =>
            {
                entity.HasKey(e => e.InscriptionId);

                entity.HasOne(d => d.Adherent)
                      .WithMany(p => p.Inscriptions)
                      .HasForeignKey(d => d.AdherentId);

                entity.HasOne(d => d.Creneau)
                      .WithMany(p => p.Inscriptions)
                      .HasForeignKey(d => d.CreneauId);
            });

            // Section Entity Configuration
            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(e => e.SectionId);
                entity.Property(e => e.SectionName).HasMaxLength(50);
            });
        }
    }
}
