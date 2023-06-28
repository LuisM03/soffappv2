using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace soffapp.Models;

public partial class Proveedor
{
    public long IdProveedor { get; set; }

    [Required(ErrorMessage ="El Nombre es requerdio")]
    [Display(Name ="Nombre")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage ="La Empresa es requerida")]
    [Display(Name = "Empresa")]
    public string Empresa { get; set; } = null!;

    [Required(ErrorMessage ="La Direccion es requerida")]
    [Display(Name = "Dirección")]
    public string Direccion { get; set; } = null!;

    [DataType(DataType.Date)]
    [Required(ErrorMessage ="La Fecha de registro es requerida ")]
    [Display(Name = "FechaRegistro")]
    public DateTime FechaRegistro { get; set; }

    [Required(ErrorMessage ="El Correo es requerido")]
    [RegularExpression("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$", ErrorMessage = "Ingrese una dirección de Correo válida")]
    [Display(Name = "Correo")]
    public string Correo { get; set; } = null!;

    [Required(ErrorMessage ="El Telefono es requerido")]
    //[RegularExpression("^\\d{10}$\r\n", ErrorMessage ="Ingrese un Telefono Valido")]
    [Display(Name = "Telefono")]
    public string Telefono { get; set; } = null!;

    [Required(ErrorMessage ="La Ciudad es requerida")]
    [Display(Name = "Ciudad")]
    public string? Ciudad { get; set; }

    [Required(ErrorMessage ="El Estado es requerido")]
    [Display(Name = "Estado")]
    public bool? Estado { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual ICollection<Insumo> Insumos { get; set; } = new List<Insumo>();
}
