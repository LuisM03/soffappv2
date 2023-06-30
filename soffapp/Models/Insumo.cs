using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace soffapp.Models;

public partial class Insumo
{

    public long IdInsumo { get; set; }


    public long IdProveedor { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    public required string Nombre { get; set; }

    [DataType(DataType.Date)]
    public DateTime FechaCaducidad { get; set; }

    [Required(ErrorMessage = "El Stock es requerido")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "El Medida es requerido")]
    public string Medida { get; set; }

    [Required(ErrorMessage = "El Precio es requerido")]
    [RegularExpression("^[0-9]+([.,][0-9]{1,2})?$",ErrorMessage = "Precio")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "El estado es requerido")]
    public bool? Estado { get; set; }

    public virtual ICollection<DetalleInsumo> DetalleInsumos { get; set; } = new List<DetalleInsumo>();

    public virtual Proveedor Proveedor { get; set; } = null!;

    public virtual ICollection<OrdenCompra> OrdenCompras { get; set; } = new List<OrdenCompra>();
}
