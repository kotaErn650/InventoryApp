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
            opt => opt.UseInMemoryDatabase("ConcertFlowDb"),
            ServiceLifetime.Singleton);

        builder.Services.AddSingleton<DbContextFactory>();

        builder.Services.AddSingleton<IConcertEventRepository, ConcertEventRepository>();
        builder.Services.AddSingleton<ConcertEventService>();

        builder.Services.AddSingleton<IArtistRepository, ArtistRepository>();
        builder.Services.AddSingleton<ArtistService>();

        builder.Services.AddTransient<DashboardViewModel>();
        builder.Services.AddTransient<EventsViewModel>();
        builder.Services.AddTransient<EventFormViewModel>();
        builder.Services.AddTransient<ArtistsViewModel>();
        builder.Services.AddTransient<ArtistFormViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();

        builder.Services.AddTransient<DashboardPage>();
        builder.Services.AddTransient<EventsPage>();
        builder.Services.AddTransient<EventFormPage>();
        builder.Services.AddTransient<ArtistsPage>();
        builder.Services.AddTransient<ArtistFormPage>();
        builder.Services.AddTransient<SettingsPage>();

        var app = builder.Build();

        SeedDatabase(app.Services);

        return app;
    }

    private static void SeedDatabase(IServiceProvider services)
    {
        var db = services.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();

        if (!db.ConcertEvents.Any())
        {
            db.ConcertEvents.AddRange(
                new ConcertEvent
                {
                    Id = Guid.NewGuid(),
                    Titulo = "Neon Nights Tour",
                    Artista = "Luna Vector",
                    Lugar = "Arena Capital",
                    Ciudad = "Bogotá",
                    FechaEvento = DateTime.Today.AddDays(12),
                    PrecioEntrada = 180000m,
                    Capacidad = 18000,
                    Estado = "Programado",
                    Destacado = true,
                    Descripcion = "Show principal con opening act y experiencia VIP."
                },
                new ConcertEvent
                {
                    Id = Guid.NewGuid(),
                    Titulo = "Pulse Fest",
                    Artista = "Distrito Sonoro",
                    Lugar = "Parque Metropolitano",
                    Ciudad = "Medellín",
                    FechaEvento = DateTime.Today.AddDays(25),
                    PrecioEntrada = 120000m,
                    Capacidad = 22000,
                    Estado = "Programado",
                    Destacado = false,
                    Descripcion = "Festival urbano con tres escenarios y food court."
                },
                new ConcertEvent
                {
                    Id = Guid.NewGuid(),
                    Titulo = "Acoustic Sessions",
                    Artista = "Valeria Norte",
                    Lugar = "Teatro Central",
                    Ciudad = "Cali",
                    FechaEvento = DateTime.Today.AddDays(6),
                    PrecioEntrada = 95000m,
                    Capacidad = 1200,
                    Estado = "Agotado",
                    Destacado = true,
                    Descripcion = "Concierto íntimo con set acústico y meet & greet."
                }
            );
            db.SaveChanges();
        }

        if (!db.Artists.Any())
        {
            db.Artists.AddRange(
                new Artist
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Luna Vector",
                    Genero = "Electropop",
                    Manager = "Camila Pérez",
                    Telefono = "3001234567",
                    Email = "booking@lunavector.com",
                    Activo = true
                },
                new Artist
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Distrito Sonoro",
                    Genero = "Urbano",
                    Manager = "Carlos Mejía",
                    Telefono = "3012223344",
                    Email = "shows@distritosonoro.com",
                    Activo = true
                },
                new Artist
                {
                    Id = Guid.NewGuid(),
                    Nombre = "Valeria Norte",
                    Genero = "Pop acústico",
                    Manager = "Laura Gómez",
                    Telefono = "3025556677",
                    Email = "contacto@valerianorte.com",
                    Activo = true
                }
            );
            db.SaveChanges();
        }
    }
}
