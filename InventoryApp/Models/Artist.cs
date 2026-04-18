namespace InventoryApp.Models;

public class Artist
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Genero { get; set; } = string.Empty;

    public string Manager { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;
}
