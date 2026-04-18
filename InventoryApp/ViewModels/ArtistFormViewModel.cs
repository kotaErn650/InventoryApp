using InventoryApp.Commands;
using InventoryApp.Models;
using InventoryApp.Services;

namespace InventoryApp.ViewModels;

public class ArtistFormViewModel : BaseViewModel, IQueryAttributable
{
    private readonly ArtistService _service;
    private Artist? _editingArtist;

    private string _nombre = string.Empty;
    public string Nombre
    {
        get => _nombre;
        set => SetProperty(ref _nombre, value);
    }

    private string _genero = string.Empty;
    public string Genero
    {
        get => _genero;
        set => SetProperty(ref _genero, value);
    }

    private string _manager = string.Empty;
    public string Manager
    {
        get => _manager;
        set => SetProperty(ref _manager, value);
    }

    private string _telefono = string.Empty;
    public string Telefono
    {
        get => _telefono;
        set => SetProperty(ref _telefono, value);
    }

    private string _email = string.Empty;
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    private bool _activo = true;
    public bool Activo
    {
        get => _activo;
        set => SetProperty(ref _activo, value);
    }

    private string _title = "Nuevo artista";
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public RelayCommand SaveCommand { get; }
    public RelayCommand GoInicioCommand { get; }
    public RelayCommand GoEventsCommand { get; }
    public RelayCommand GoArtistsCommand { get; }
    public RelayCommand GoSettingsCommand { get; }

    public ArtistFormViewModel(ArtistService service)
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
        if (query.TryGetValue("artistId", out var raw) &&
            Guid.TryParse(raw?.ToString(), out var id))
        {
            LoadArtist(id).ContinueWith(t =>
            {
                if (t.IsFaulted)
                    System.Diagnostics.Debug.WriteLine($"[ArtistFormViewModel] Error loading artist: {t.Exception}");
            }, TaskScheduler.Default);
        }
    }

    private async Task LoadArtist(Guid id)
    {
        var artist = await _service.GetById(id);
        if (artist is null)
            return;

        _editingArtist = artist;
        Nombre = artist.Nombre;
        Genero = artist.Genero;
        Manager = artist.Manager;
        Telefono = artist.Telefono;
        Email = artist.Email;
        Activo = artist.Activo;
        Title = "Editar artista";
    }

    private async Task Save()
    {
        if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Genero))
        {
            await Shell.Current.DisplayAlertAsync("Error", "Completa el nombre y el género musical.", "OK");
            return;
        }

        if (!string.IsNullOrWhiteSpace(Email) && !Email.Contains('@'))
        {
            await Shell.Current.DisplayAlertAsync("Error", "El correo electrónico no es válido.", "OK");
            return;
        }

        if (_editingArtist is not null)
        {
            _editingArtist.Nombre = Nombre;
            _editingArtist.Genero = Genero;
            _editingArtist.Manager = Manager;
            _editingArtist.Telefono = Telefono;
            _editingArtist.Email = Email;
            _editingArtist.Activo = Activo;
            await _service.Update(_editingArtist);
        }
        else
        {
            await _service.Create(new Artist
            {
                Id = Guid.NewGuid(),
                Nombre = Nombre,
                Genero = Genero,
                Manager = Manager,
                Telefono = Telefono,
                Email = Email,
                Activo = Activo
            });
        }

        await Shell.Current.GoToAsync("..");
    }
}
