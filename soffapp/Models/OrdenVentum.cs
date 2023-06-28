using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace soffapp.Models;

public partial class OrdenVentum
{
    public long IdOrden { get; set; }

    [Required(ErrorMessage="Este campo es obligatorio")]
    public long IdVenta { get; set; }

    [Required(ErrorMessage="El producto es obligatorio")]
    public long IdProducto { get; set; }
    [Remote(action: "Create", controller:"OrdenVenta")]
    [Required(ErrorMessage="Debe ingresar una cantidad de la orden")]
    [Range(1, int.MaxValue, ErrorMessage="Debe ser mayor a uno")]
    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Total { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
