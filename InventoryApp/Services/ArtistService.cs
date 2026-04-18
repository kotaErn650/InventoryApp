using InventoryApp.Models;
using InventoryApp.Repositories;

namespace InventoryApp.Services;

public class ArtistService
{
    private readonly IArtistRepository _repository;

    public ArtistService(IArtistRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Artist>> GetArtists()
    {
        return _repository.GetAll();
    }

    public Task<Artist?> GetById(Guid id)
    {
        return _repository.GetById(id);
    }

    public Task Create(Artist artist)
    {
        return _repository.Add(artist);
    }

    public Task Update(Artist artist)
    {
        return _repository.Update(artist);
    }

    public Task ToggleActivo(Artist artist)
    {
        artist.Activo = !artist.Activo;
        return _repository.Update(artist);
    }
}
