using System;
using System.Collections.Generic;

namespace soffapp.Models;

public partial class Compra
{
    public long IdCompra { get; set; }

    public long IdProveedor { get; set; }

    public DateTime FechaCompra { get; set; }

    public decimal Total { get; set; }

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    public virtual ICollection<OrdenCompra> OrdenCompras { get; set; } = new List<OrdenCompra>();
}
