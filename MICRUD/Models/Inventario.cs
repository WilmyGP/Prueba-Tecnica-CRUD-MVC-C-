using System;
using System.Collections.Generic;
using System.Data;

namespace MICRUD.Models;

public partial class Inventario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? Cantidad { get; set; }

    public DateTime? FechaEntrada = DateTime.Now;

    public DateTime? FechaSalida { get; set; }

    public int? cantidadVendidos { get; set; }    
}
