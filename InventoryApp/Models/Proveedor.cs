namespace InventoryApp.Models;

public class Proveedor
{
    public Guid Id { get; set; }

    public string Foto { get; set; } = "";

    public string Nombre { get; set; } = "";

    public string TipoProducto { get; set; } = "";

    public string Telefono { get; set; } = "";

    public string Email { get; set; } = "";

    public bool Activo { get; set; } = true;
}
