using System;
using System.Collections.Generic;

namespace SportAssoWebApp.Models;

public partial class Adherent
{
    public int AdherentId { get; set; }
    
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public bool? IsEncadrant { get; set; }

    public bool IsValidated { get; set; } = false; // Default: not validated

    public virtual ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
}
