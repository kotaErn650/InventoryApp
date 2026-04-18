using InventoryApp.Commands;
using InventoryApp.Models;
using InventoryApp.Services;

namespace InventoryApp.ViewModels;

public class EventsViewModel : BaseViewModel
{
    private readonly ConcertEventService _service;
    private readonly List<ConcertEvent> _allEvents = new();
    private string _searchText = string.Empty;

    public IEnumerable<ConcertEvent> Events => GetFilteredEvents();

    public string SearchText
    {
        get => _searchText;
        set
        {
            SetProperty(ref _searchText, value);
            OnPropertyChanged(nameof(Events));
        }
    }

    public RelayCommand LoadCommand { get; }
    public RelayCommand ToggleFeaturedCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand NewCommand { get; }
    public RelayCommand GoInicioCommand { get; }
    public RelayCommand GoEventsCommand { get; }
    public RelayCommand GoArtistsCommand { get; }
    public RelayCommand GoSettingsCommand { get; }

    public EventsViewModel(ConcertEventService service)
    {
        _service = service;

        LoadCommand = new RelayCommand(async _ => await Load());
        ToggleFeaturedCommand = new RelayCommand(async e => await ToggleFeatured((ConcertEvent)e!));
        EditCommand = new RelayCommand(async e => await Edit((ConcertEvent)e!));
        NewCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("eventform"));
        GoInicioCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("//dashboard"));
        GoEventsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("events"));
        GoArtistsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("artists"));
        GoSettingsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("settings"));
    }

    public async Task Load()
    {
        _allEvents.Clear();
        _allEvents.AddRange(await _service.GetEvents());
        OnPropertyChanged(nameof(Events));
    }

    private IEnumerable<ConcertEvent> GetFilteredEvents()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
            return _allEvents;

        return _allEvents.Where(concertEvent =>
            concertEvent.Titulo.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
            concertEvent.Artista.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
            concertEvent.Lugar.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
            concertEvent.Ciudad.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
    }

    private async Task ToggleFeatured(ConcertEvent concertEvent)
    {
        await _service.ToggleFeatured(concertEvent);
        await Load();
    }

    private Task Edit(ConcertEvent concertEvent)
    {
        return Shell.Current.GoToAsync($"eventform?eventId={concertEvent.Id}");
    }
}
