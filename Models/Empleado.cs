using System;
using System.Collections.Generic;

namespace taller_final_cruds.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public decimal ValorHora { get; set; }

    public string? Email { get; set; }

    public string Telefono { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public DateTime FechaIngreso { get; set; }

    public bool? Estado { get; set; }
}
