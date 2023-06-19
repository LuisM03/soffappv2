using System;
using System.Collections.Generic;

namespace soffapp.Models;

public partial class Producto
{
    public long IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public bool? Estado { get; set; }
}
