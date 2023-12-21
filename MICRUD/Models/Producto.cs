using System;
using System.Collections.Generic;

namespace MICRUD.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public double? Precio { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public int? Cantidad { get; set; }
}
