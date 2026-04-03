using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Data;

public class DbContextFactory
{
    private readonly DbContextOptions<AppDbContext> _options;

    public DbContextFactory(DbContextOptions<AppDbContext> options)
    {
        _options = options;
    }

    public AppDbContext Create() => new AppDbContext(_options);
}
