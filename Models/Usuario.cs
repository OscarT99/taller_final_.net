using System;
using System.Collections.Generic;

namespace taller_final_cruds.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? Estado { get; set; }
}
