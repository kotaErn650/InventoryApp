using System;
using System.Collections.Generic;
using System.Text;

using InventoryApp.Models;
using InventoryApp.Repositories;

namespace InventoryApp.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _repository.GetAll();
    }

    public async Task Create(Product product)
    {
        await _repository.Add(product);
    }

    public async Task Update(Product product)
    {
        await _repository.Update(product);
    }

    public async Task Disable(Product product)
    {
        product.Activo = false;
        await _repository.Update(product);
    }
}