using InventoryApp.Data;
using InventoryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Repositories;

public class ConcertEventRepository : IConcertEventRepository
{
    private readonly AppDbContext _context;

    public ConcertEventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ConcertEvent>> GetAll()
    {
        return await _context.ConcertEvents
            .OrderBy(concertEvent => concertEvent.FechaEvento)
            .ToListAsync();
    }

    public async Task<ConcertEvent?> GetById(Guid id)
    {
        return await _context.ConcertEvents.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Add(ConcertEvent concertEvent)
    {
        _context.ConcertEvents.Add(concertEvent);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ConcertEvent concertEvent)
    {
        _context.ConcertEvents.Update(concertEvent);
        await _context.SaveChangesAsync();
    }
}
