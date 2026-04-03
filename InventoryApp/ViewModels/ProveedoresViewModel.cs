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

    public ProveedoresViewModel(ProveedorService service)
    {
        _service = service;
        LoadCommand = new RelayCommand(async _ => await Load());
    }

    public async Task Load()
    {
        Proveedores.Clear();

        var list = await _service.GetProveedores();

        foreach (var item in list)
            Proveedores.Add(item);
    }
}
