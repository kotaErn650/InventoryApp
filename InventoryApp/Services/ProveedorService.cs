using InventoryApp.Models;
using InventoryApp.Repositories;

namespace InventoryApp.Services;

public class ProveedorService
{
    private readonly IProveedorRepository _repository;

    public ProveedorService(IProveedorRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Proveedor>> GetProveedores()
    {
        return await _repository.GetAll();
    }
}
