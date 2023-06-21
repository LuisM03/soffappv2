using System;
using System.Collections.Generic;

namespace soffapp.Models;

public partial class AsociacionProducto
{
    public long IdAsociacion { get; set; }

    public long IdDetalleInsumo { get; set; }

    public long IdProducto { get; set; }

    public virtual DetalleInsumo IdDetalleInsumoNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
