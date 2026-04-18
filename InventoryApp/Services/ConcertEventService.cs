using InventoryApp.Models;
using InventoryApp.Repositories;

namespace InventoryApp.Services;

public class ConcertEventService
{
    private readonly IConcertEventRepository _repository;

    public ConcertEventService(IConcertEventRepository repository)
    {
        _repository = repository;
    }

    public Task<List<ConcertEvent>> GetEvents()
    {
        return _repository.GetAll();
    }

    public Task<ConcertEvent?> GetById(Guid id)
    {
        return _repository.GetById(id);
    }

    public Task Create(ConcertEvent concertEvent)
    {
        return _repository.Add(concertEvent);
    }

    public Task Update(ConcertEvent concertEvent)
    {
        return _repository.Update(concertEvent);
    }

    public Task ToggleFeatured(ConcertEvent concertEvent)
    {
        concertEvent.Destacado = !concertEvent.Destacado;
        return _repository.Update(concertEvent);
    }
}
