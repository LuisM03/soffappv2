using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace soffapp.Models;

public partial class Producto
{
    public long IdProducto { get; set; }

    [Required(ErrorMessage ="Campo obligatorio")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "Campo obligatorio")]

    public decimal Precio { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<AsociacionProducto> AsociacionProductos { get; set; } = new List<AsociacionProducto>();

    public virtual ICollection<OrdenVentum> OrdenVenta { get; set; } = new List<OrdenVentum>();
}
