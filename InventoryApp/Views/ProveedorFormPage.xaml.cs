using InventoryApp.ViewModels;

namespace InventoryApp.Views;

public partial class ProveedorFormPage : ContentPage
{
    public ProveedorFormPage(ProveedorFormViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
