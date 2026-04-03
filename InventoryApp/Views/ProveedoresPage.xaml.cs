using InventoryApp.ViewModels;

namespace InventoryApp.Views;

public partial class ProveedoresPage : ContentPage
{
    public ProveedoresPage(ProveedoresViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ProveedoresViewModel vm)
            vm.LoadCommand.Execute(null);
    }
}
