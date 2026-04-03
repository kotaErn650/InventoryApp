using InventoryApp.Models;
using InventoryApp.Services;
using InventoryApp.Commands;

namespace InventoryApp.ViewModels;

public class ProductFormViewModel : BaseViewModel, IQueryAttributable
{
    private readonly ProductService _service;
    private Product? _editingProduct;

    private string _nombre = "";
    public string Nombre
    {
        get => _nombre;
        set => SetProperty(ref _nombre, value);
    }

    private string _descripcion = "";
    public string Descripcion
    {
        get => _descripcion;
        set => SetProperty(ref _descripcion, value);
    }

    private decimal _precio;
    public decimal Precio
    {
        get => _precio;
        set => SetProperty(ref _precio, value);
    }

    private int _stock;
    public int Stock
    {
        get => _stock;
        set => SetProperty(ref _stock, value);
    }

    private bool _activo = true;
    public bool Activo
    {
        get => _activo;
        set => SetProperty(ref _activo, value);
    }

    private string _title = "Nuevo Producto";
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public RelayCommand SaveCommand { get; }
    public RelayCommand GoInicioCommand { get; }
    public RelayCommand GoProductsCommand { get; }
    public RelayCommand GoProveedoresCommand { get; }

    public ProductFormViewModel(ProductService service)
    {
        _service = service;
        SaveCommand = new RelayCommand(async _ => await Save());
        GoInicioCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("//dashboard"));
        GoProductsCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("products"));
        GoProveedoresCommand = new RelayCommand(async _ => await Shell.Current.GoToAsync("proveedores"));
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("productId", out var raw) &&
            Guid.TryParse(raw?.ToString(), out var id))
        {
            LoadProduct(id)
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                        System.Diagnostics.Debug.WriteLine($"[ProductFormViewModel] Error loading product: {t.Exception}");
                }, TaskScheduler.Default);
        }
    }

    private async Task LoadProduct(Guid id)
    {
        var product = await _service.GetById(id);
        if (product is null) return;

        _editingProduct = product;
        Nombre = product.Nombre;
        Descripcion = product.Descripcion;
        Precio = product.Precio;
        Stock = product.Stock;
        Activo = product.Activo;
        Title = "Editar Producto";
    }

    private async Task Save()
    {
        if (_editingProduct != null)
        {
            _editingProduct.Nombre = Nombre;
            _editingProduct.Descripcion = Descripcion;
            _editingProduct.Precio = Precio;
            _editingProduct.Stock = Stock;
            _editingProduct.Activo = Activo;
            await _service.Update(_editingProduct);
        }
        else
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Nombre = Nombre,
                Descripcion = Descripcion,
                Precio = Precio,
                Stock = Stock,
                Activo = Activo,
                FechaCreacion = DateTime.UtcNow
            };
            await _service.Create(product);
        }

        await Shell.Current.GoToAsync("..");
    }
}
