using InventoryApp.Models;
using InventoryApp.Services;
using InventoryApp.Commands;

namespace InventoryApp.ViewModels;

public class ProveedoresViewModel : BaseViewModel
{
    private readonly ProveedorService _service;
    private readonly List<Proveedor> _allProveedores = new();
    private string _searchText = string.Empty;

    public IEnumerable<Proveedor> Proveedores => GetFilteredProveedores();
    public string SearchText
    {
        get => _searchText;
        set
        {
            SetProperty(ref _searchText, value);
            OnPropertyChanged(nameof(Proveedores));
        }
    }

    public RelayCommand LoadCommand { get; }
    public RelayCommand ToggleActivoCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand NewCommand { get; }
    public RelayCommand GoInicioCommand { get; }
    public RelayCommand GoProductsCommand { get; }
    public RelayCommand GoProveedoresCommand { get; }
    public RelayCommand GoConfiguracionCommand { get; }

    public ProveedoresViewModel(ProveedorService service)
    {
        _service = service;

        LoadCommand = new RelayCommand(async _ => await Load());
        ToggleActivoCommand = new RelayCommand(async p => await ToggleActivo((Proveedor)p!));
        EditCommand = new RelayCommand(async p => await Edit((Proveedor)p!));
        NewCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("proveedorform"));
        GoInicioCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("//dashboard"));
        GoProductsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("products"));
        GoProveedoresCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("proveedores"));
        GoConfiguracionCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("configuracion"));
    }

    public async Task Load()
    {
        _allProveedores.Clear();

        var list = await _service.GetProveedores();
        _allProveedores.AddRange(list);
        OnPropertyChanged(nameof(Proveedores));
    }

    private IEnumerable<Proveedor> GetFilteredProveedores()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
            return _allProveedores;

        return _allProveedores.Where(proveedor =>
            (proveedor.Nombre?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
            (proveedor.TipoProducto?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
            (proveedor.Telefono?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
            (proveedor.Email?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false));
    }

    private async Task ToggleActivo(Proveedor proveedor)
    {
        await _service.ToggleActivo(proveedor);
        await Load();
    }

    private async Task Edit(Proveedor proveedor)
    {
        await Shell.Current.GoToAsync($"proveedorform?proveedorId={proveedor.Id}");
    }
}
