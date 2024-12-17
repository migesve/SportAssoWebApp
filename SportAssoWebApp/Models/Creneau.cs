using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportAssoWebApp.Models;

public partial class Creneau
{
    public int CreneauId { get; set; }

    public int? SectionId { get; set; }

    public string? Lieu { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Required]
    [DataType(DataType.Time)]
    public TimeSpan Hour { get; set; }


    public int? PlacesMax { get; set; }

    public int? PlacesRestantes { get; set; }

    public decimal? Price { get; set; }

    public string? DocumentsRequired { get; set; }

    public virtual ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();

    public virtual Section? Section { get; set; }

    //public required Section Section { get; set; } // Navigation Property
}

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }
}
