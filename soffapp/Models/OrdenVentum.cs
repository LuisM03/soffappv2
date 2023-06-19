using System;
using System.Collections.Generic;

namespace soffapp.Models;

public partial class OrdenVentum
{
    public long IdOrden { get; set; }

    public long IdVenta { get; set; }

    public long IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Total { get; set; }
}
