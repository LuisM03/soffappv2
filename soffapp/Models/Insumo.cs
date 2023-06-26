using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace soffapp.Models;

public partial class Insumo
{

    public long IdInsumo { get; set; }


    public long IdProveedor { get; set; }

    [Required]
    public string Nombre { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateTime FechaCaducidad { get; set; }

    [Required]
    public int Stock { get; set; }

    [Required]
    public string Medida { get; set; } = null!;

    [Required]
    public decimal Precio { get; set; }
    

    public bool? Estado { get; set; }

    public virtual ICollection<DetalleInsumo> DetalleInsumos { get; set; } = new List<DetalleInsumo>();

    public virtual Proveedor Proveedor { get; set; } = null!;

    public virtual ICollection<OrdenCompra> OrdenCompras { get; set; } = new List<OrdenCompra>();
}
