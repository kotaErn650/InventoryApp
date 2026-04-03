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

    public async Task<Proveedor?> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task Create(Proveedor proveedor)
    {
        await _repository.Add(proveedor);
    }

    public async Task Update(Proveedor proveedor)
    {
        await _repository.Update(proveedor);
    }

    public async Task ToggleActivo(Proveedor proveedor)
    {
        proveedor.Activo = !proveedor.Activo;
        await _repository.Update(proveedor);
    }
}
