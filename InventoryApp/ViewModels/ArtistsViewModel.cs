using InventoryApp.Commands;
using InventoryApp.Models;
using InventoryApp.Services;

namespace InventoryApp.ViewModels;

public class ArtistsViewModel : BaseViewModel
{
    private readonly ArtistService _service;
    private readonly List<Artist> _allArtists = new();
    private string _searchText = string.Empty;

    public IEnumerable<Artist> Artists => GetFilteredArtists();

    public string SearchText
    {
        get => _searchText;
        set
        {
            SetProperty(ref _searchText, value);
            OnPropertyChanged(nameof(Artists));
        }
    }

    public RelayCommand LoadCommand { get; }
    public RelayCommand ToggleActivoCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand NewCommand { get; }
    public RelayCommand GoInicioCommand { get; }
    public RelayCommand GoEventsCommand { get; }
    public RelayCommand GoArtistsCommand { get; }
    public RelayCommand GoSettingsCommand { get; }

    public ArtistsViewModel(ArtistService service)
    {
        _service = service;

        LoadCommand = new RelayCommand(async _ => await Load());
        ToggleActivoCommand = new RelayCommand(async artist => await ToggleActivo((Artist)artist!));
        EditCommand = new RelayCommand(async artist => await Edit((Artist)artist!));
        NewCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("artistform"));
        GoInicioCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("//dashboard"));
        GoEventsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("events"));
        GoArtistsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("artists"));
        GoSettingsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("settings"));
    }

    public async Task Load()
    {
        _allArtists.Clear();
        _allArtists.AddRange(await _service.GetArtists());
        OnPropertyChanged(nameof(Artists));
    }

    private IEnumerable<Artist> GetFilteredArtists()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
            return _allArtists;

        return _allArtists.Where(artist =>
            artist.Nombre.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
            artist.Genero.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
            artist.Manager.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
            artist.Email.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
    }

    private async Task ToggleActivo(Artist artist)
    {
        await _service.ToggleActivo(artist);
        await Load();
    }

    private Task Edit(Artist artist)
    {
        return Shell.Current.GoToAsync($"artistform?artistId={artist.Id}");
    }
}
