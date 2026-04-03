using System.Collections.ObjectModel;
using InventoryApp.Models;
using InventoryApp.Services;
using InventoryApp.Commands;

namespace InventoryApp.ViewModels;

public class ProveedoresViewModel : BaseViewModel
{
    private readonly ProveedorService _service;

    public ObservableCollection<Proveedor> Proveedores { get; set; } = new();

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
        Proveedores.Clear();

        var list = await _service.GetProveedores();

        foreach (var item in list)
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
