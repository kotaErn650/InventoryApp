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
}
