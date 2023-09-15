using System;
using System.Collections.Generic;

namespace taller_final_cruds.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public int Pedido { get; set; }

    public DateTime FechaVenta { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public decimal ValorTotal { get; set; }

    public string FormaDePago { get; set; } = null!;

    public string Observaciones { get; set; } = null!;

    public string? Estado { get; set; }
}
