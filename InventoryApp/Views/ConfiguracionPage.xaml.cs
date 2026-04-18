using InventoryApp.ViewModels;

namespace InventoryApp.Views;

public partial class ConfiguracionPage : ContentPage
{
    public ConfiguracionPage(ConfiguracionViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
