using InventoryApp.Data;
using InventoryApp.Models;
using InventoryApp.Repositories;
using InventoryApp.Services;
using InventoryApp.ViewModels;
using InventoryApp.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<InventoryApp.App>();

        // Configurar la ruta de la base de datos SQLite
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "InventoryApp.db");

        builder.Services.AddDbContext<AppDbContext>(
            opt => opt.UseSqlite($"Data Source={dbPath}"),
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
        builder.Services.AddTransient<ConfiguracionViewModel>();

        builder.Services.AddTransient<DashboardPage>();
        builder.Services.AddTransient<ProductsPage>();
        builder.Services.AddTransient<ProductFormPage>();
        builder.Services.AddTransient<ProveedoresPage>();
        builder.Services.AddTransient<ProveedorFormPage>();
        builder.Services.AddTransient<ConfiguracionPage>();

        var app = builder.Build();

        // Inicializar la base de datos de forma asincrónica
        var dbContext = app.Services.GetRequiredService<AppDbContext>();
        _ = DatabaseInitializer.InitializeAsync(dbContext);

        return app;
    }
}
