using InventoryApp.Data;
using InventoryApp.Models;
using InventoryApp.Repositories;
using InventoryApp.Services;
using InventoryApp.ViewModels;
using InventoryApp.Views;
using Microsoft.EntityFrameworkCore;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<InventoryApp.App>();

        builder.Services.AddDbContext<AppDbContext>(
            opt => opt.UseInMemoryDatabase("InventoryDb"),
            ServiceLifetime.Singleton);

        builder.Services.AddSingleton<DbContextFactory>();

        builder.Services.AddSingleton<IProductRepository, ProductRepository>();
        builder.Services.AddSingleton<ProductService>();

        builder.Services.AddSingleton<IProveedorRepository, ProveedorRepository>();
        builder.Services.AddSingleton<ProveedorService>();

        builder.Services.AddTransient<DashboardViewModel>();
        builder.Services.AddTransient<ProductsViewModel>();
        builder.Services.AddTransient<ProductFormViewModel>();
        builder.Services.AddTransient<ProveedoresViewModel>();
        builder.Services.AddTransient<ProveedorFormViewModel>();

        builder.Services.AddTransient<DashboardPage>();
        builder.Services.AddTransient<ProductsPage>();
        builder.Services.AddTransient<ProductFormPage>();
        builder.Services.AddTransient<ProveedoresPage>();
        builder.Services.AddTransient<ProveedorFormPage>();

        var app = builder.Build();

        SeedDatabase(app.Services);

        return app;
    }

    private static void SeedDatabase(IServiceProvider services)
    {
        var db = services.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();

        if (!db.Products.Any())
        {
            db.Products.AddRange(
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
            );
            db.SaveChanges();
        }

        if (!db.Proveedores.Any())
        {
            db.Proveedores.AddRange(
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
                    Email= "aajdha@Ecci.edu.co"
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
            );
            db.SaveChanges();
        }
    }
}

