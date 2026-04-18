using InventoryApp.Commands;
using InventoryApp.Models;
using InventoryApp.Services;

namespace InventoryApp.ViewModels;

public class EventFormViewModel : BaseViewModel, IQueryAttributable
{
    private readonly ConcertEventService _service;
    private ConcertEvent? _editingEvent;

    private string _titulo = string.Empty;
    public string Titulo
    {
        get => _titulo;
        set => SetProperty(ref _titulo, value);
    }

    private string _artista = string.Empty;
    public string Artista
    {
        get => _artista;
        set => SetProperty(ref _artista, value);
    }

    private string _lugar = string.Empty;
    public string Lugar
    {
        get => _lugar;
        set => SetProperty(ref _lugar, value);
    }

    private string _ciudad = string.Empty;
    public string Ciudad
    {
        get => _ciudad;
        set => SetProperty(ref _ciudad, value);
    }

    private DateTime _fechaEvento = DateTime.Today.AddDays(7);
    public DateTime FechaEvento
    {
        get => _fechaEvento;
        set => SetProperty(ref _fechaEvento, value);
    }

    private decimal _precioEntrada;
    public decimal PrecioEntrada
    {
        get => _precioEntrada;
        set => SetProperty(ref _precioEntrada, value);
    }

    private int _capacidad = 1000;
    public int Capacidad
    {
        get => _capacidad;
        set => SetProperty(ref _capacidad, value);
    }

    private string _estado = "Programado";
    public string Estado
    {
        get => _estado;
        set => SetProperty(ref _estado, value);
    }

    private bool _destacado;
    public bool Destacado
    {
        get => _destacado;
        set => SetProperty(ref _destacado, value);
    }

    private string _descripcion = string.Empty;
    public string Descripcion
    {
        get => _descripcion;
        set => SetProperty(ref _descripcion, value);
    }

    private string _title = "Nuevo evento";
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public IReadOnlyList<string> EstadoOptions { get; } = new[] { "Programado", "Agotado", "Reprogramado", "Cancelado" };

    public RelayCommand SaveCommand { get; }
    public RelayCommand GoInicioCommand { get; }
    public RelayCommand GoEventsCommand { get; }
    public RelayCommand GoArtistsCommand { get; }
    public RelayCommand GoSettingsCommand { get; }

    public EventFormViewModel(ConcertEventService service)
    {
        _service = service;
        SaveCommand = new RelayCommand(async _ => await Save());
        GoInicioCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("//dashboard"));
        GoEventsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("events"));
        GoArtistsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("artists"));
        GoSettingsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("settings"));
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("eventId", out var raw) &&
            Guid.TryParse(raw?.ToString(), out var id))
        {
            LoadEvent(id).ContinueWith(t =>
            {
                if (t.IsFaulted)
                    System.Diagnostics.Debug.WriteLine($"[EventFormViewModel] Error loading event: {t.Exception}");
            }, TaskScheduler.Default);
        }
    }

    private async Task LoadEvent(Guid id)
    {
        var concertEvent = await _service.GetById(id);
        if (concertEvent is null)
            return;

        _editingEvent = concertEvent;
        Titulo = concertEvent.Titulo;
        Artista = concertEvent.Artista;
        Lugar = concertEvent.Lugar;
        Ciudad = concertEvent.Ciudad;
        FechaEvento = concertEvent.FechaEvento;
        PrecioEntrada = concertEvent.PrecioEntrada;
        Capacidad = concertEvent.Capacidad;
        Estado = concertEvent.Estado;
        Destacado = concertEvent.Destacado;
        Descripcion = concertEvent.Descripcion;
        Title = "Editar evento";
    }

    private async Task Save()
    {
        if (string.IsNullOrWhiteSpace(Titulo) || string.IsNullOrWhiteSpace(Artista) ||
            string.IsNullOrWhiteSpace(Lugar) || string.IsNullOrWhiteSpace(Ciudad))
        {
            await Shell.Current.DisplayAlertAsync("Error", "Completa título, artista, lugar y ciudad.", "OK");
            return;
        }

        if (Capacidad <= 0)
        {
            await Shell.Current.DisplayAlertAsync("Error", "La capacidad debe ser mayor que cero.", "OK");
            return;
        }

        if (PrecioEntrada < 0)
        {
            await Shell.Current.DisplayAlertAsync("Error", "El precio no puede ser negativo.", "OK");
            return;
        }

        if (_editingEvent is not null)
        {
            _editingEvent.Titulo = Titulo;
            _editingEvent.Artista = Artista;
            _editingEvent.Lugar = Lugar;
            _editingEvent.Ciudad = Ciudad;
            _editingEvent.FechaEvento = FechaEvento;
            _editingEvent.PrecioEntrada = PrecioEntrada;
            _editingEvent.Capacidad = Capacidad;
            _editingEvent.Estado = Estado;
            _editingEvent.Destacado = Destacado;
            _editingEvent.Descripcion = Descripcion;
            await _service.Update(_editingEvent);
        }
        else
        {
            await _service.Create(new ConcertEvent
            {
                Id = Guid.NewGuid(),
                Titulo = Titulo,
                Artista = Artista,
                Lugar = Lugar,
                Ciudad = Ciudad,
                FechaEvento = FechaEvento,
                PrecioEntrada = PrecioEntrada,
                Capacidad = Capacidad,
                Estado = Estado,
                Destacado = Destacado,
                Descripcion = Descripcion
            });
        }

        await Shell.Current.GoToAsync("..");
    }
}
