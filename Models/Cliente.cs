using System;
using System.Collections.Generic;

namespace taller_final_cruds.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string TipoCliente { get; set; } = null!;

    public string TipoIdentificacion { get; set; } = null!;

    public string NumeroIdentificacion { get; set; } = null!;

    public string RazonSocial { get; set; } = null!;

    public string NombreComercial { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string? Contacto { get; set; }

    public string Telefono { get; set; } = null!;

    public string? Email { get; set; }

    public bool? Estado { get; set; }
}
