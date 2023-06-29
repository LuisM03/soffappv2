using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace soffapp.Models;

public partial class DetalleInsumo
{
    public long IdDetalle { get; set; }
    [Required(ErrorMessage = "Campo requerido")]

    public long IdInsumo { get; set; }
    [Required(ErrorMessage = "Campo requerido")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 1")]

    public int Cantidad { get; set; }
    [Required(ErrorMessage = "Campo requerido")]

    public string Medida { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<AsociacionProducto> AsociacionProductos { get; set; } = new List<AsociacionProducto>();

    public virtual Insumo? IdInsumoNavigation { get; set; } = null!;
}
