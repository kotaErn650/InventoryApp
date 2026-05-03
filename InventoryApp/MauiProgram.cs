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
        builder.Services.AddTransient<ConfiguracionViewModel>();

        builder.Services.AddTransient<DashboardPage>();
        builder.Services.AddTransient<ProductsPage>();
        builder.Services.AddTransient<ProductFormPage>();
        builder.Services.AddTransient<ProveedoresPage>();
        builder.Services.AddTransient<ProveedorFormPage>();
        builder.Services.AddTransient<ConfiguracionPage>();

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
                    Nombre = "Festival de Música Aurora",
                    Descripcion = "Concierto nocturno con tres escenarios, zona gastronómica y experiencias inmersivas.",
                    Precio = 180000m,
                    Stock = 1200,
                    Activo = true,
                    FechaCreacion = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Tech Summit 2026",
                    Descripcion = "Jornada de innovación con charlas sobre IA, producto digital y networking ejecutivo.",
                    Precio = 95000m,
                    Stock = 450,
                    Activo = true,
                    FechaCreacion = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Mercado Creativo de Fin de Semana",
                    Descripcion = "Encuentro de marcas emergentes con talleres, food trucks y música en vivo.",
                    Precio = 25000m,
                    Stock = 800,
                    Activo = true,
                    FechaCreacion = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Bootcamp Startups Live",
                    Descripcion = "Sesiones prácticas para emprendedores con pitch deck clinic y mentorías express.",
                    Precio = 65000m,
                    Stock = 180,
                    Activo = false,
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
                    Foto = "organizer_music.svg",
                    Nombre = "Luna Stage Productions",
                    TipoProducto = "Conciertos y festivales",
                    Activo = true,
                    Telefono = "3001234567",
                    Email = "booking@lunastage.co"
                },
                new Proveedor
                {
                    Id = Guid.NewGuid(),
                    Foto = "organizer_business.svg",
                    Nombre = "Nodo Conference Lab",
                    TipoProducto = "Congresos corporativos",
                    Activo = true,
                    Telefono = "3007654321",
                    Email = "hello@nodolab.co"
                },
                new Proveedor
                {
                    Id = Guid.NewGuid(),
                    Foto = "organizer_community.svg",
                    Nombre = "Ciudad Viva Events",
                    TipoProducto = "Experiencias de comunidad",
                    Activo = true,
                    Telefono = "3015558899",
                    Email = "team@ciudadviva.co"
                },
                new Proveedor
                {
                    Id = Guid.NewGuid(),
                    Foto = "organizer_default.svg",
                    Nombre = "Atelier Social Club",
                    TipoProducto = "Eventos boutique",
                    Activo = true,
                    Telefono = "3204567812",
                    Email = "contacto@ateliersocial.co"
                }
            );
            db.SaveChanges();
        }
    }
}
