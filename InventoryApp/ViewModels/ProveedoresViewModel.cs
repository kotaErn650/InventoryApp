using System.Collections.ObjectModel;
using InventoryApp.Models;
using InventoryApp.Services;
using InventoryApp.Commands;

namespace InventoryApp.ViewModels;

public class ProveedoresViewModel : BaseViewModel
{
    private readonly ProveedorService _service;
    private readonly List<Proveedor> _allProveedores = new();
    private string _searchText = string.Empty;

    public ObservableCollection<Proveedor> Proveedores { get; set; } = new();
    public string SearchText
    {
        get => _searchText;
        set
        {
            SetProperty(ref _searchText, value);
            ApplyFilter();
        }
    }

    public RelayCommand LoadCommand { get; }
    public RelayCommand ToggleActivoCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand NewCommand { get; }
    public RelayCommand GoInicioCommand { get; }
    public RelayCommand GoProductsCommand { get; }
    public RelayCommand GoProveedoresCommand { get; }

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
    }

    public async Task Load()
    {
        _allProveedores.Clear();
        Proveedores.Clear();

        var list = await _service.GetProveedores();
        _allProveedores.AddRange(list);
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        Proveedores.Clear();

        IEnumerable<Proveedor> filtered = _allProveedores;

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filtered = _allProveedores.Where(proveedor =>
                proveedor.Nombre.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                proveedor.TipoProducto.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                proveedor.Telefono.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                proveedor.Email.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        foreach (var item in filtered)
            Proveedores.Add(item);
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
