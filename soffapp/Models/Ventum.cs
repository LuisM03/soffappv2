using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace soffapp.Models;

public partial class Ventum
{
    public long IdVenta { get; set; }

    public DateTime FechaVenta { get; set; }

    [Required(ErrorMessage="Debe escoger un método de pago")]
    public string Metodo { get; set; } = null!;

    public decimal Total { get; set; }

    [Required(ErrorMessage = "Debe escoger un tipo de venta")]
    public string TipoVenta { get; set; } = null!;

    public virtual ICollection<OrdenVentum> OrdenVenta { get; set; } = new List<OrdenVentum>();
}
