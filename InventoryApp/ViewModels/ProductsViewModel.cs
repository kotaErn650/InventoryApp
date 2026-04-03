using System.Collections.ObjectModel;
using InventoryApp.Models;
using InventoryApp.Services;
using InventoryApp.Commands;

namespace InventoryApp.ViewModels;

public class ProductsViewModel : BaseViewModel
{
    private readonly ProductService _service;

    public ObservableCollection<Product> Products { get; set; } = new();

    public RelayCommand LoadCommand { get; }
    public RelayCommand DisableCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand NewCommand { get; }

    public ProductsViewModel(ProductService service)
    {
        _service = service;

        LoadCommand = new RelayCommand(async _ => await Load());
        DisableCommand = new RelayCommand(async p => await Disable((Product)p!));
        EditCommand = new RelayCommand(async p => await Edit((Product)p!));
        NewCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("productform"));
    }

    public async Task Load()
    {
        Products.Clear();

        var list = await _service.GetProducts();

        foreach (var item in list)
            Products.Add(item);
    }

    private async Task Disable(Product product)
    {
        await _service.Disable(product);
        await Load();
    }

    private async Task Edit(Product product)
    {
        await Shell.Current.GoToAsync($"productform?productId={product.Id}");
    }
}
