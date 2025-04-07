using System;
using System.Collections.Generic;

namespace PaginaWebNad.Models;

public partial class TErpgrupoSistema
{
    public int IdErpgrupoSistema { get; set; }

    public int IdErpgrupo { get; set; }

    public int IdSistema { get; set; }

    public int? AppCreada { get; set; }

    public int? ScriptCreado { get; set; }

    public string? SNombreAdicional { get; set; }

    public virtual TErpgrupo IdErpgrupoNavigation { get; set; } = null!;

    public virtual CSistema IdSistemaNavigation { get; set; } = null!;

    public virtual ICollection<TrRolesSistemaUsuariosGrupo> TrRolesSistemaUsuariosGrupos { get; set; } = new List<TrRolesSistemaUsuariosGrupo>();
}
