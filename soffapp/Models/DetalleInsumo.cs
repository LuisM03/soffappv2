using System;
using System.Collections.Generic;

namespace soffapp.Models;

public partial class DetalleInsumo
{
    public long IdDetalle { get; set; }

    public long IdInsumo { get; set; }

    public int Cantidad { get; set; }

    public string Medida { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<AsociacionProducto> AsociacionProductos { get; set; } = new List<AsociacionProducto>();

    public virtual Insumo IdInsumoNavigation { get; set; } = null!;
}
