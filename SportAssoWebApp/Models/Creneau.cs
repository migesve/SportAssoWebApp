using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportAssoWebApp.Models;

public partial class Creneau
{
    public int CreneauId { get; set; }

    public int? SectionId { get; set; }

    public string? Lieu { get; set; }

    public DateTime? Horaire { get; set; }

    public int? PlacesMax { get; set; }

    public int? PlacesRestantes { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    public string? DocumentsRequired { get; set; }

    public virtual ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();

    public virtual Section? Section { get; set; }
}
