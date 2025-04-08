using System;
using System.Collections.Generic;

namespace PaginaWebNad.Models;

public partial class TUsuarioSistemaRol
{
    public int IIdUsuarioSistemaRol { get; set; }

    public int IIdUsuario { get; set; }

    public int IdSistema { get; set; }

    public int IIdRol { get; set; }

    public virtual CtRole IIdRolNavigation { get; set; } = null!;

    public virtual TUsuario IIdUsuarioNavigation { get; set; } = null!;

    public virtual CSistema IdSistemaNavigation { get; set; } = null!;
}
