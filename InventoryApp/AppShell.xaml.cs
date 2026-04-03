using InventoryApp.Views;

namespace InventoryApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("products", typeof(ProductsPage));
        Routing.RegisterRoute("productform", typeof(ProductFormPage));
    }
}
