using InventoryApp.Models;
using InventoryApp.Services;
using InventoryApp.Commands;

namespace InventoryApp.ViewModels;

public class ProductsViewModel : BaseViewModel
{
    private readonly ProductService _service;
    private readonly List<Product> _allProducts = new();
    private string _searchText = string.Empty;

    public IEnumerable<Product> Products => GetFilteredProducts();
    public string SearchText
    {
        get => _searchText;
        set
        {
            SetProperty(ref _searchText, value);
            OnPropertyChanged(nameof(Products));
        }
    }

    public RelayCommand LoadCommand { get; }
    public RelayCommand DisableCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand NewCommand { get; }
    public RelayCommand GoInicioCommand { get; }
    public RelayCommand GoProductsCommand { get; }
    public RelayCommand GoProveedoresCommand { get; }
    public RelayCommand GoConfiguracionCommand { get; }

    public ProductsViewModel(ProductService service)
    {
        _service = service;

        LoadCommand = new RelayCommand(async _ => await Load());
        DisableCommand = new RelayCommand(async p => await Disable((Product)p!));
        EditCommand = new RelayCommand(async p => await Edit((Product)p!));
        NewCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("productform"));
        GoInicioCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("//dashboard"));
        GoProductsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("products"));
        GoProveedoresCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("proveedores"));
        GoConfiguracionCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("configuracion"));
    }

    public async Task Load()
    {
        _allProducts.Clear();

        var list = await _service.GetProducts();
        _allProducts.AddRange(list);
        OnPropertyChanged(nameof(Products));
    }

    private IEnumerable<Product> GetFilteredProducts()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
            return _allProducts;

        return _allProducts.Where(product =>
            (product.Nombre?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
            (product.Descripcion?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false));
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
