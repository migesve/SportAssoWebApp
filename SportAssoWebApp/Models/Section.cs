using System;
using System.Collections.Generic;

namespace SportAssoWebApp.Models;

public partial class Section
{
    public int SectionId { get; set; }

    public string? SectionName { get; set; }

    public virtual ICollection<Creneau> Creneauxes { get; set; } = new List<Creneau>();
}
