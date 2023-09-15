using System;
using System.Collections.Generic;

namespace taller_final_cruds.Models;

public partial class Compra
{
    public int IdCompra { get; set; }

    public int Proveedor { get; set; }

    public string? Contacto { get; set; }

    public string NumeroFactura { get; set; } = null!;

    public DateTime FechaDeCompra { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Iva { get; set; }

    public decimal? ValorTotal { get; set; }

    public string FormaDePago { get; set; } = null!;

    public string? Observaciones { get; set; }

    public bool? Estado { get; set; }
}
