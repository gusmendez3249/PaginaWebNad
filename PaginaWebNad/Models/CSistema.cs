using System;
using System.Collections.Generic;

namespace PaginaWebNad.Models;

public partial class CSistema
{
    public int IdSistema { get; set; }

    public string NomSistema { get; set; } = null!;

    public string? Url { get; set; }

    public string Nomenglatura { get; set; } = null!;

    public virtual ICollection<TErpgrupoSistema> TErpgrupoSistemas { get; set; } = new List<TErpgrupoSistema>();

    public virtual ICollection<TrRolesSistema> TrRolesSistemas { get; set; } = new List<TrRolesSistema>();
}
