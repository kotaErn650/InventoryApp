using InventoryApp.Models;
using InventoryApp.Services;
using InventoryApp.Commands;

namespace InventoryApp.ViewModels;

public class ProveedorFormViewModel : BaseViewModel, IQueryAttributable
{
    private readonly ProveedorService _service;
    private Proveedor? _editingProveedor;

    private string _nombre = "";
    public string Nombre
    {
        get => _nombre;
        set => SetProperty(ref _nombre, value);
    }

    private string _tipoProducto = "";
    public string TipoProducto
    {
        get => _tipoProducto;
        set => SetProperty(ref _tipoProducto, value);
    }

    private string _telefono = "";
    public string Telefono
    {
        get => _telefono;
        set => SetProperty(ref _telefono, value);
    }

    private string _email = "";
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

    private string _title = "Nuevo Proveedor";
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public RelayCommand SaveCommand { get; }

    public ProveedorFormViewModel(ProveedorService service)
    {
        _service = service;
        SaveCommand = new RelayCommand(async _ => await Save());
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("proveedorId", out var raw) &&
            Guid.TryParse(raw?.ToString(), out var id))
        {
            LoadProveedor(id).ContinueWith(t =>
            {
                if (t.IsFaulted)
                    System.Diagnostics.Debug.WriteLine($"[ProveedorFormViewModel] Error loading proveedor: {t.Exception}");
            }, TaskScheduler.Default);
        }
    }

    private async Task LoadProveedor(Guid id)
    {
        var proveedor = await _service.GetById(id);
        if (proveedor is null) return;

        _editingProveedor = proveedor;
        Nombre = proveedor.Nombre;
        TipoProducto = proveedor.TipoProducto;
        Telefono = proveedor.Telefono;
        Email = proveedor.Email;
        Activo = proveedor.Activo;
        Title = "Editar Proveedor";
    }

    private async Task Save()
    {
        if (string.IsNullOrWhiteSpace(Nombre))
        {
            await Shell.Current.DisplayAlertAsync("Error", "El nombre del proveedor es obligatorio.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(TipoProducto))
        {
            await Shell.Current.DisplayAlertAsync("Error", "El tipo de producto es obligatorio.", "OK");
            return;
        }

        if (!string.IsNullOrWhiteSpace(Email) &&
            !Email.Contains('@'))
        {
            await Shell.Current.DisplayAlertAsync("Error", "El correo electrónico no es válido.", "OK");
            return;
        }

        if (_editingProveedor != null)
        {
            _editingProveedor.Nombre = Nombre;
            _editingProveedor.TipoProducto = TipoProducto;
            _editingProveedor.Telefono = Telefono;
            _editingProveedor.Email = Email;
            _editingProveedor.Activo = Activo;
            await _service.Update(_editingProveedor);
        }
        else
        {
            var proveedor = new Proveedor
            {
                Id = Guid.NewGuid(),
                Nombre = Nombre,
                TipoProducto = TipoProducto,
                Telefono = Telefono,
                Email = Email,
                Activo = Activo
            };
            await _service.Create(proveedor);
        }

        await Shell.Current.GoToAsync("..");
    }
}
