using InventoryApp.Data;
using InventoryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Repositories;

public class ArtistRepository : IArtistRepository
{
    private readonly AppDbContext _context;

    public ArtistRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Artist>> GetAll()
    {
        return await _context.Artists
            .OrderBy(artist => artist.Nombre)
            .ToListAsync();
    }

    public async Task<Artist?> GetById(Guid id)
    {
        return await _context.Artists.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Add(Artist artist)
    {
        _context.Artists.Add(artist);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Artist artist)
    {
        _context.Artists.Update(artist);
        await _context.SaveChangesAsync();
    }
}
