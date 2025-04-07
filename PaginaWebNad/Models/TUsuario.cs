using System;
using System.Collections.Generic;

namespace PaginaWebNad.Models;

public partial class TUsuario
{
    public int IIdUsuario { get; set; }

    public string SUsuario { get; set; } = null!;

    public string SCorreo { get; set; } = null!;

    public int IEstatus { get; set; }

    public virtual ICollection<TrRolesSistemaUsuariosGrupo> TrRolesSistemaUsuariosGrupos { get; set; } = new List<TrRolesSistemaUsuariosGrupo>();
}
