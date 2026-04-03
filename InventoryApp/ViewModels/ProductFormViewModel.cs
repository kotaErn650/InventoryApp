using System;
using System.Collections.Generic;
using System.Text;

using InventoryApp.Models;
using InventoryApp.Services;
using InventoryApp.Commands;

namespace InventoryApp.ViewModels;

public class ProductFormViewModel : BaseViewModel
{
    private readonly ProductService _service;

    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public RelayCommand SaveCommand { get; }

    public ProductFormViewModel(ProductService service)
    {
        _service = service;

        SaveCommand = new RelayCommand(async _ => await Save());
    }

    private async Task Save()
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Nombre = Nombre,
            Descripcion = Descripcion,
            Precio = Precio,
            Stock = Stock,
            Activo = true
        };

        await _service.Create(product);

        await Shell.Current.GoToAsync("..");
    }
}
