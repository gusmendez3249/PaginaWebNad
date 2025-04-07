using System;
using System.Collections.Generic;

namespace PaginaWebNad.Models;

public partial class TErpgrupo
{
    public int IdErpgrupo { get; set; }

    public string NomGrupo { get; set; } = null!;

    public string? UrlErp { get; set; }

    public virtual ICollection<TErpgrupoSistema> TErpgrupoSistemas { get; set; } = new List<TErpgrupoSistema>();
}
