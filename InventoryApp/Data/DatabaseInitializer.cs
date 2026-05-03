using Microsoft.EntityFrameworkCore;
using InventoryApp.Models;

namespace InventoryApp.Data;

public class DatabaseInitializer
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        try
        {
            
            await context.Database.EnsureCreatedAsync();

            // Seed de datos si la base de datos está vacía
            if (!await context.Products.AnyAsync())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Nombre = "Laptop",
                        Descripcion = "Laptop Dell XPS 15",
                        Precio = 3500m,
                        Stock = 10,
                        Activo = true,
                        FechaCreacion = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Nombre = "Mouse",
                        Descripcion = "Mouse inalámbrico Logitech",
                        Precio = 80000m,
                        Stock = 50,
                        Activo = true,
                        FechaCreacion = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Nombre = "Teclado",
                        Descripcion = "Teclado mecánico RGB",
                        Precio = 150000,
                        Stock = 30,
                        Activo = true,
                        FechaCreacion = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Nombre = "Monitor",
                        Descripcion = "Monitor 27\" 4K UHD",
                        Precio = 800M,
                        Stock = 5,
                        Activo = true,
                        FechaCreacion = DateTime.UtcNow
                    }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }

            if (!await context.Proveedores.AnyAsync())
            {
                var proveedores = new List<Proveedor>
                {
                    new Proveedor
                    {
                        Id = Guid.NewGuid(),
                        Foto = "hard.png",
                        Nombre = "TechDistribuciones S.A.",
                        TipoProducto = "Electrónica",
                        Activo = true
                    },
                    new Proveedor
                    {
                        Id = Guid.NewGuid(),
                        Foto = "offi.png",
                        Nombre = "OfficeSupplies Ltda.",
                        TipoProducto = "Papelería y Oficina",
                        Activo = true,
                        Telefono = "3212222",
                        Email = "aajdha@Ecci.edu.co"
                    },
                    new Proveedor
                    {
                        Id = Guid.NewGuid(),
                        Foto = "hard.png",
                        Nombre = "Hardware Pro",
                        TipoProducto = "Componentes de Computadora",
                        Activo = true,
                        Telefono = "3212222",
                        Email = "aajdha@Ecci.edu.co"
                    },
                    new Proveedor
                    {
                        Id = Guid.NewGuid(),
                        Foto = "per.png",
                        Nombre = "MegaImport Corp.",
                        TipoProducto = "Perifericos",
                        Activo = true,
                        Telefono = "3212222",
                        Email = "aajdha@Ecci.edu.co"
                    }
                };

                await context.Proveedores.AddRangeAsync(proveedores);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error initializing database: {ex.Message}");
            throw;
        }
    }
}