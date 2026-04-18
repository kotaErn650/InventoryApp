using InventoryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<ConcertEvent> ConcertEvents => Set<ConcertEvent>();
    public DbSet<Artist> Artists => Set<Artist>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
