using Microsoft.Extensions.Logging;



using InventoryApp.Data;
using InventoryApp.Repositories;
using InventoryApp.Services;
using InventoryApp.ViewModels;
using InventoryApp.Views;
using Microsoft.EntityFrameworkCore;
using InventoryApp;


public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>();

        builder.Services.AddDbContext<AppDbContext>(opt =>
            opt.UseInMemoryDatabase("InventoryDb"));

        builder.Services.AddScoped<IProductRepository, ProductRepository>();

        builder.Services.AddScoped<ProductService>();

        builder.Services.AddTransient<ProductsViewModel>();
        builder.Services.AddTransient<ProductFormViewModel>();

        builder.Services.AddTransient<ProductsPage>();
        builder.Services.AddTransient<ProductFormPage>();
        builder.Services.AddTransient<DashboardPage>();

        return builder.Build();
    }
}
