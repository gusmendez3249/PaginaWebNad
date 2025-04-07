using System;
using System.Collections.Generic;

namespace PaginaWebNad.Models;

public partial class TrRolesSistemaUsuariosGrupo
{
    public int IIdRolesSistemaUsuarios { get; set; }

    public int IIdRolSistema { get; set; }

    public int IdUsuario { get; set; }

    public int IdErpgrupoSistema { get; set; }

    public virtual TrRolesSistema IIdRolSistemaNavigation { get; set; } = null!;

    public virtual TErpgrupoSistema IdErpgrupoSistemaNavigation { get; set; } = null!;

    public virtual TUsuario IdUsuarioNavigation { get; set; } = null!;
}
