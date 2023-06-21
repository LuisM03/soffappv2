using System;
using System.Collections.Generic;

namespace soffapp.Models;

public partial class Ventum
{
    public long IdVenta { get; set; }

    public DateTime FechaVenta { get; set; }

    public string Metodo { get; set; } = null!;

    public decimal Total { get; set; }

    public string TipoVenta { get; set; } = null!;

    public virtual ICollection<OrdenVentum> OrdenVenta { get; set; } = new List<OrdenVentum>();
}
