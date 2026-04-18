namespace InventoryApp.Models;

public class ConcertEvent
{
    public Guid Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Artista { get; set; } = string.Empty;

    public string Lugar { get; set; } = string.Empty;

    public string Ciudad { get; set; } = string.Empty;

    public DateTime FechaEvento { get; set; } = DateTime.Today.AddDays(7);

    public decimal PrecioEntrada { get; set; }

    public int Capacidad { get; set; }

    public string Estado { get; set; } = "Programado";

    public bool Destacado { get; set; }

    public string Descripcion { get; set; } = string.Empty;
}
