using InventoryApp.Models;

namespace InventoryApp.Repositories;

public interface IConcertEventRepository
{
    Task<List<ConcertEvent>> GetAll();
    Task<ConcertEvent?> GetById(Guid id);
    Task Add(ConcertEvent concertEvent);
    Task Update(ConcertEvent concertEvent);
}
