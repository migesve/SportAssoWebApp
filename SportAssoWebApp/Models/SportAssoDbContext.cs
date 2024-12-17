using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SportAssoWebApp.Models;

public partial class SportAssoDbContext : DbContext
{
    public SportAssoDbContext()
    {
    }

    public SportAssoDbContext(DbContextOptions<SportAssoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adherent> Adherents { get; set; }

    public virtual DbSet<Creneau> Creneaux { get; set; }

    public virtual DbSet<Inscription> Inscriptions { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-SIDE7HB;Database=MSSQLSERVER01;Integrated Security=True;TrustServerCertificate=True;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adherent>(entity =>
        {
            entity.HasKey(e => e.AdherentId).HasName("PK__Adherent__97189F0106640CDF");

            entity.HasIndex(e => e.Email, "UQ__Adherent__A9D105348B178F61").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.IsEncadrant).HasDefaultValue(false);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
        });

        modelBuilder.Entity<Creneau>(entity =>
        {
            entity.HasKey(e => e.CreneauId).HasName("PK__Creneaux__17AA69153C83E3FF");

            entity.ToTable("Creneaux");

            entity.Property(e => e.Horaire).HasColumnType("datetime");
            entity.Property(e => e.Lieu).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Section).WithMany(p => p.Creneaux)
                .HasForeignKey(d => d.SectionId)
                .HasConstraintName("FK__Creneaux__Sectio__3D5E1FD2");
        });

        modelBuilder.Entity<Inscription>(entity =>
        {
            entity.HasKey(e => e.InscriptionId).HasName("PK__Inscript__3332B6760C79B1B0");

            entity.HasOne(d => d.Adherent).WithMany(p => p.Inscriptions)
                .HasForeignKey(d => d.AdherentId)
                .HasConstraintName("FK__Inscripti__Adher__403A8C7D");

            entity.HasOne(d => d.Creneau).WithMany(p => p.Inscriptions)
                .HasForeignKey(d => d.CreneauId)
                .HasConstraintName("FK__Inscripti__Crene__412EB0B6");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.SectionId).HasName("PK__Sections__80EF08720DDDCA53");

            entity.Property(e => e.SectionName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
