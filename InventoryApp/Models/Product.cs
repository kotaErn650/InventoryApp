using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryApp.Models;

public class Product
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = "";

    public string Descripcion { get; set; } = "";

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public bool Activo { get; set; } = true;

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}