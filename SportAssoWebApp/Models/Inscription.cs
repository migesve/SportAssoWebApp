using System;
using System.Collections.Generic;

namespace SportAssoWebApp.Models;

public partial class Inscription
{
    public int InscriptionId { get; set; }

    public int? AdherentId { get; set; }

    public int? CreneauId { get; set; }

    public bool? IsPaid { get; set; }

    public string? DocumentsSubmitted { get; set; }

    public virtual Adherent? Adherent { get; set; }

    public virtual Creneau? Creneau { get; set; }
}
