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

        builder.Services.AddTransient<DashboardViewModel>();
        builder.Services.AddTransient<ProductsViewModel>();
        builder.Services.AddTransient<ProductFormViewModel>();

        builder.Services.AddTransient<DashboardPage>();
        builder.Services.AddTransient<ProductsPage>();
        builder.Services.AddTransient<ProductFormPage>();

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
                    Precio = 80m,
                    Stock = 50,
                    Activo = true,
                    FechaCreacion = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Teclado",
                    Descripcion = "Teclado mecánico RGB",
                    Precio = 150m,
                    Stock = 30,
                    Activo = true,
                    FechaCreacion = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Monitor",
                    Descripcion = "Monitor 27\" 4K UHD",
                    Precio = 800m,
                    Stock = 5,
                    Activo = true,
                    FechaCreacion = DateTime.UtcNow
                }
            );
            db.SaveChanges();
        }
    }
}

