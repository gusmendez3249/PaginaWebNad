using System;
using System.Collections.Generic;

namespace PaginaWebNad.Models;

public partial class Cliente
{
    public int IIdCliente { get; set; }

    public string SNombre { get; set; } = null!;

    public virtual ICollection<TErpgrupo> TErpgrupos { get; set; } = new List<TErpgrupo>();
}
