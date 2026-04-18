using InventoryApp.Commands;
using InventoryApp.Services;

namespace InventoryApp.ViewModels;

public class DashboardViewModel : BaseViewModel
{
    private readonly ConcertEventService _concertEventService;
    private readonly ArtistService _artistService;

    private int _totalEventos;
    public int TotalEventos
    {
        get => _totalEventos;
        set => SetProperty(ref _totalEventos, value);
    }

    private int _eventosDestacados;
    public int EventosDestacados
    {
        get => _eventosDestacados;
        set => SetProperty(ref _eventosDestacados, value);
    }

    private int _artistasActivos;
    public int ArtistasActivos
    {
        get => _artistasActivos;
        set => SetProperty(ref _artistasActivos, value);
    }

    private string _proximoEvento = "Sin eventos programados";
    public string ProximoEvento
    {
        get => _proximoEvento;
        set => SetProperty(ref _proximoEvento, value);
    }

    public RelayCommand LoadCommand { get; }
    public RelayCommand GoEventsCommand { get; }
    public RelayCommand GoInicioCommand { get; }
    public RelayCommand GoArtistsCommand { get; }
    public RelayCommand GoSettingsCommand { get; }

    public DashboardViewModel(ConcertEventService concertEventService, ArtistService artistService)
    {
        _concertEventService = concertEventService;
        _artistService = artistService;

        LoadCommand = new RelayCommand(async _ => await Load());
        GoEventsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("events"));
        GoInicioCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("//dashboard"));
        GoArtistsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("artists"));
        GoSettingsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("settings"));
    }

    public async Task Load()
    {
        var events = await _concertEventService.GetEvents();
        var artists = await _artistService.GetArtists();
        var upcomingEvent = events
            .Where(x => x.FechaEvento >= DateTime.Today)
            .OrderBy(x => x.FechaEvento)
            .FirstOrDefault();

        TotalEventos = events.Count;
        EventosDestacados = events.Count(x => x.Destacado);
        ArtistasActivos = artists.Count(x => x.Activo);
        ProximoEvento = upcomingEvent is null
            ? "Sin eventos programados"
            : $"{upcomingEvent.Titulo} · {upcomingEvent.FechaEvento:dd MMM} en {upcomingEvent.Ciudad}";
    }
}
