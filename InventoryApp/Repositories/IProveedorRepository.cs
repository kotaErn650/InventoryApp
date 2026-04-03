using InventoryApp.Models;

namespace InventoryApp.Repositories;

public interface IProveedorRepository
{
    Task<List<Proveedor>> GetAll();
    Task<Proveedor?> GetById(Guid id);
    Task Add(Proveedor proveedor);
    Task Update(Proveedor proveedor);
}
