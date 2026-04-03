using System;
using System.Collections.Generic;
using System.Text;
using InventoryApp.Models;

namespace InventoryApp.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAll();

    Task<Product?> GetById(Guid id);

    Task Add(Product product);

    Task Update(Product product);
}