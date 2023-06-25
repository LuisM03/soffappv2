using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace soffapp.Models;

public partial class Insumo
{
    
    public long IdInsumo { get; set; }
    
    public long IdProveedor { get; set; }
    
    public string Nombre { get; set; } = null!;
    
    public DateTime FechaCaducidad { get; set; }
    
    public int Stock { get; set; }
    
    public string Medida { get; set; } = null!;
    
    public decimal Precio { get; set; }
    
    public bool? Estado { get; set; }

    public virtual ICollection<DetalleInsumo> DetalleInsumos { get; set; } = new List<DetalleInsumo>();

    public virtual Proveedor Proveedor { get; set; } = null!;

    public virtual ICollection<OrdenCompra> OrdenCompras { get; set; } = new List<OrdenCompra>();
}
