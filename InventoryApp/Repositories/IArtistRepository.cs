using InventoryApp.Models;

namespace InventoryApp.Repositories;

public interface IArtistRepository
{
    Task<List<Artist>> GetAll();
    Task<Artist?> GetById(Guid id);
    Task Add(Artist artist);
    Task Update(Artist artist);
}
