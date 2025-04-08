using System;
using System.Collections.Generic;

namespace PaginaWebNad.Models;

public partial class CtRole
{
    public int IIdRol { get; set; }

    public string SRol { get; set; } = null!;

    public string SDescripcion { get; set; } = null!;

    public int IEstatus { get; set; }

    public virtual ICollection<TUsuarioSistemaRol> TUsuarioSistemaRols { get; set; } = new List<TUsuarioSistemaRol>();

    public virtual ICollection<TrRolesSistema> TrRolesSistemas { get; set; } = new List<TrRolesSistema>();
}
