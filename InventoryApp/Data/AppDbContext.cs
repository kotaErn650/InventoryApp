using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using InventoryApp.Models;

namespace InventoryApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(

            new Product
            {
                Id = Guid.NewGuid(),
                Nombre = "Laptop",
                Descripcion = "Laptop Dell",
                Precio = 3500,
                Stock = 10,
                Activo = true
            },

            new Product
            {
                Id = Guid.NewGuid(),
                Nombre = "Mouse",
                Descripcion = "Mouse Logitech",
                Precio = 80,
                Stock = 50,
                Activo = true
            },

            new Product
            {
                Id = Guid.NewGuid(),
                Nombre = "Teclado",
                Descripcion = "Teclado mecánico",
                Precio = 150,
                Stock = 30,
                Activo = true
            }
        );
    }
}
