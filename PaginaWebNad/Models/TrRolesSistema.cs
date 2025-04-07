using System;
using System.Collections.Generic;

namespace PaginaWebNad.Models;

public partial class TrRolesSistema
{
    public int IIdRolSistema { get; set; }

    public int IIdRol { get; set; }

    public int IdSistema { get; set; }

    public virtual CtRole IIdRolNavigation { get; set; } = null!;

    public virtual CSistema IdSistemaNavigation { get; set; } = null!;

    public virtual ICollection<TrRolesSistemaUsuariosGrupo> TrRolesSistemaUsuariosGrupos { get; set; } = new List<TrRolesSistemaUsuariosGrupo>();
}
