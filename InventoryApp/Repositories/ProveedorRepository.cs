using InventoryApp.Models;
using InventoryApp.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Repositories;

public class ProveedorRepository : IProveedorRepository
{
    private readonly AppDbContext _context;

    public ProveedorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Proveedor>> GetAll()
    {
        return await _context.Proveedores.ToListAsync();
    }

    public async Task<Proveedor?> GetById(Guid id)
    {
        return await _context.Proveedores.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Add(Proveedor proveedor)
    {
        _context.Proveedores.Add(proveedor);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Proveedor proveedor)
    {
        _context.Proveedores.Update(proveedor);
        await _context.SaveChangesAsync();
    }
}
