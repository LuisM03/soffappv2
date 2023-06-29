using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace soffapp.Models;

public partial class OrdenCompra
{
    public long IdOrden { get; set; }
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public long IdCompra { get; set; }
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public long IdInsumo { get; set; }
    [Remote(action: "Create", controller: "OrdenVenta")]
    [Required(ErrorMessage = "Debe ingresar una cantidad de la orden")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe ser mayor a uno")]

    public string Cantidad { get; set; } = null!;

    public decimal PrecioUnitario { get; set; }

    public decimal Total { get; set; }

    public virtual Compra IdCompraNavigation { get; set; } = null!;

    public virtual Insumo IdInsumoNavigation { get; set; } = null!;
}
