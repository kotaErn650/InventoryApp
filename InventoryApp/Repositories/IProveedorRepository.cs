using InventoryApp.Models;

namespace InventoryApp.Repositories;

public interface IProveedorRepository
{
    Task<List<Proveedor>> GetAll();
}
